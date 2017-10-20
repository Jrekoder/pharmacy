using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;
using Pharmacy.Domain;
using Pharmacy.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Functions.Console
{
    public class Program
    {
        private static DocumentClient client_document = new DocumentClient(new Uri(Settings.DocumentDB), Settings.DocumentDBAuthKey);

        private static void Main(string[] args)
        {
            PrintMenu();
        }

        private static async void PrintMenu()
        {
            string option = string.Empty;
            do
            {
                option = string.Empty;
                System.Console.Clear();

                int r = 214;
                int g = 37;
                int b = 250;

                Colorful.Console.WriteAscii("PHARMACY", Color.FromArgb(r, g, b));

                Colorful.Console.WriteLine("1.- Register random product", Color.FromArgb(r, g, b));
                r -= 18; b -= 9;
                Colorful.Console.WriteLine("2.- Register random pharmacy", Color.FromArgb(r, g, b));
                r -= 18; b -= 9;
                Colorful.Console.WriteLine("3.- Update product availability", Color.FromArgb(r, g, b));
                r -= 18; b -= 9;
                Colorful.Console.WriteLine("4.- Search product availability", Color.FromArgb(r, g, b));
                r -= 18; b -= 9;
                Colorful.Console.WriteLine("5.- Request new token to get mobile settings", Color.FromArgb(r, g, b));
                r -= 18; b -= 9;
                Colorful.Console.WriteLine("6.- Exit", Color.FromArgb(r, g, b));
                r -= 18; b -= 9;

                Colorful.Console.Write("\nPick an option:", Color.FromArgb(r, g, b));
                option = System.Console.ReadLine();

                switch (option)
                {
                    case "1":
                        System.Console.WriteLine(RegisterRandomProduct());
                        break;

                    case "2":
                        System.Console.WriteLine(RegisterRandomPharmacy());
                        break;

                    case "3":
                        var res = UpdateProductAvailability().Result;
                        System.Console.WriteLine(res);
                        System.Console.ReadKey();
                        break;

                    case "4":
                        System.Console.Write("\nProduct name:");
                        string productName = System.Console.ReadLine();
                        ProductAvailabilityFilter filter = new ProductAvailabilityFilter();
                        filter.ProductName = productName;
                        string sdresult = await SearchProduct(filter);
                        System.Console.WriteLine(sdresult);
                        System.Console.ReadKey();
                        break;

                    case "5":
                        string sresult = await RequestMobileSettings();
                        System.Console.WriteLine(sresult);
                        System.Console.ReadKey();
                        break;
                }
            }
            while (option != "6");
        }

        private static async Task<string> RegisterRandomProduct()
        {
            Domain.Product product = new Domain.Product();
            product.Name = Domain.Product.GetRandomName();
            product.Presentation = Domain.Product.GetRandomPresentation();
            product.Sustance = Domain.Product.GetRandomSustance();
            product.ExpirationDate = Domain.Product.GetRandomExpirationDate();
            product.ImageUrl = Domain.Product.GetRandomImageUrl();

            await client_document.CreateDatabaseIfNotExistsAsync(new Database { Id = Settings.DatabaseId });
            var collection = await client_document.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(Settings.DatabaseId), new DocumentCollection { Id = Settings.ProductsCollectionId }, new RequestOptions { OfferThroughput = 1000 });
            var result = await client_document.CreateDocumentAsync(collection.Resource.SelfLink, product);
            var document = result.Resource;
            return document.ToString();
        }

        private static async Task<string> RegisterRandomPharmacy()
        {
            Domain.Pharmacy pharmacy = new Domain.Pharmacy();
            pharmacy.Name = Domain.Pharmacy.GetRandomName();
            pharmacy.Address = Domain.Pharmacy.GetRandomAddress();
            pharmacy.District = "Álvaro Obregón";
            pharmacy.City = "Ciudad de México";
            pharmacy.State = "Ciudad de México";
            pharmacy.Country = "México";

            Tuple<double, double> location = Domain.Pharmacy.GetRandomLatLong();
            pharmacy.Latitude = location.Item1;
            pharmacy.Longitude = location.Item2;

            await client_document.CreateDatabaseIfNotExistsAsync(new Database { Id = Settings.DatabaseId });
            var collection = await client_document.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(Settings.DatabaseId), new DocumentCollection { Id = Settings.PharmaciesCollectionId }, new RequestOptions { OfferThroughput = 1000 });
            var result = await client_document.CreateDocumentAsync(collection.Resource.SelfLink, pharmacy);
            var document = result.Resource;
            return document.ToString();
        }

        private static async Task<string> UpdateProductAvailability()
        {
            string result = string.Empty;
            DeleteProductAvailabilityCollection();

            List<Domain.Product> products = null;
            var collection_products = await client_document.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(Settings.DatabaseId), new DocumentCollection { Id = Settings.ProductsCollectionId }, new RequestOptions { OfferThroughput = 1000 });
            var query_products = client_document.CreateDocumentQuery<Domain.Product>(collection_products.Resource.SelfLink, new SqlQuerySpec()
            {
                QueryText = "SELECT * FROM Products"
            });
            products = query_products.ToList();

            List<Domain.Pharmacy> pharmacies = null;
            var collection_pharmacies = await client_document.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(Settings.DatabaseId), new DocumentCollection { Id = Settings.PharmaciesCollectionId }, new RequestOptions { OfferThroughput = 1000 });
            var query_pharmacies = client_document.CreateDocumentQuery<Domain.Pharmacy>(collection_pharmacies.Resource.SelfLink, new SqlQuerySpec()
            {
                QueryText = "SELECT * FROM Pharmacies"
            });
            pharmacies = query_pharmacies.ToList();

            for (int i = 0; i <= 99; i++)
            {
                ProductAvailability productAvailability = new ProductAvailability();
                Random rnd = new Random(DateTime.Now.Millisecond);
                var random_product = products[rnd.Next(0, products.Count)];
                var random_pharmacy = pharmacies[rnd.Next(0, pharmacies.Count)];

                productAvailability.PharmacyId = random_pharmacy.Id;
                productAvailability.ProductId = random_product.Id;
                productAvailability.Quantity = rnd.Next(0, 100);

                await client_document.CreateDatabaseIfNotExistsAsync(new Database { Id = Settings.DatabaseId });
                var collection_productAvailability = await client_document.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(Settings.DatabaseId), new DocumentCollection { Id = Settings.ProductAvailabilityCollectionId }, new RequestOptions { OfferThroughput = 1000 });
                var result_productAvailability = await client_document.CreateDocumentAsync(collection_productAvailability.Resource.SelfLink, productAvailability);
            }

            return "Updated successfully!";
        }

        private static async void DeleteProductAvailabilityCollection()
        {
            await client_document.CreateDatabaseIfNotExistsAsync(new Database { Id = Settings.DatabaseId });
            var collection = await client_document.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(Settings.DatabaseId), new DocumentCollection { Id = Settings.ProductAvailabilityCollectionId }, new RequestOptions { OfferThroughput = 1000 });
            var result = await client_document.DeleteDocumentCollectionAsync(collection.Resource.SelfLink);
        }

        public static async Task<string> SearchProduct(ProductAvailabilityFilter filter)
        {
            byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            byte[] key = Guid.NewGuid().ToByteArray();
            var token = Convert.ToBase64String(time.Concat(key).ToArray());
            token = SecurityHelper.Encrypt(token, Settings.CryptographyKey);
            System.Console.WriteLine($"Token: {token}");

            ProductAvailabilityRequest request = new ProductAvailabilityRequest();
            request.Token = token;
            request.Filter = filter;

            using (var client = new HttpClient())
            {
                var service = $"http://localhost:7071/api/ProductAvailability/";
                byte[] byteData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(request));
                using (var content = new ByteArrayContent(byteData))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var httpResponse = client.PostAsync(service, content).Result;

                    if (httpResponse.StatusCode == HttpStatusCode.OK)
                    {
                        System.Console.WriteLine(httpResponse.StatusCode);
                        return await httpResponse.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        System.Console.WriteLine(httpResponse.StatusCode);
                    }
                }
            }
            return null;
        }

        public static async Task<string> RequestMobileSettings()
        {
            string token = string.Empty;

            byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            byte[] key = Guid.NewGuid().ToByteArray();
            token = Convert.ToBase64String(time.Concat(key).ToArray());
            token = SecurityHelper.Encrypt(token, Settings.CryptographyKey);
            System.Console.WriteLine($"Token: {token}");

            MobileSettingsRequest request = new MobileSettingsRequest();
            request.Token = token;

            using (var client = new HttpClient())
            {
                var service = $"http://localhost:7071/api/MobileSettings/";
                byte[] byteData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(request));
                using (var content = new ByteArrayContent(byteData))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var httpResponse = client.PostAsync(service, content).Result;

                    if (httpResponse.StatusCode == HttpStatusCode.OK)
                    {
                        System.Console.WriteLine(httpResponse.StatusCode);
                        return await httpResponse.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        System.Console.WriteLine(httpResponse.StatusCode);
                    }
                }
            }
            return null;
        }
    }
}