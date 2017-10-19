using CognitiveLocator.Helpers;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Pharmacy.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pharmacy.Functions
{
    public static class ProductAvailability
    {
        private static DocumentClient client_document = new DocumentClient(new Uri(Settings.DocumentDB), Settings.DocumentDBAuthKey);

        [FunctionName(nameof(ProductAvailability))]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "ProductAvailability/")]HttpRequestMessage req, TraceWriter log)
        {
            Domain.ProductAvailabilityRequest request = await req.Content.ReadAsAsync<Domain.ProductAvailabilityRequest>();

            var decrypted_token = SecurityHelper.Decrypt(request.Token, Settings.CryptographyKey);

            byte[] data = Convert.FromBase64String(decrypted_token);
            DateTime when = DateTime.FromBinary(BitConverter.ToInt64(data, 0));
            if (when < DateTime.UtcNow.AddMinutes(-5))
            {
                return req.CreateResponse(HttpStatusCode.BadRequest);
            }

            if (request.Filter == null)
            {
                return req.CreateResponse(HttpStatusCode.BadRequest, "Filter is required to perform the search");
            }

            List<Domain.Product> products = null;
            var collection_products = await client_document.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(Settings.DatabaseId), new DocumentCollection { Id = Settings.ProductsCollectionId }, new RequestOptions { OfferThroughput = 1000 });
            var query_products = client_document.CreateDocumentQuery<Domain.Product>(collection_products.Resource.SelfLink, new SqlQuerySpec()
            {
                QueryText = $"SELECT * FROM Products p WHERE CONTAINS(UPPER(p.name), UPPER('{request.Filter.ProductName}'))"
            });
            products = query_products.ToList();

            List<Domain.Pharmacy> pharmacies = null;
            var collection_pharmacies = await client_document.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(Settings.DatabaseId), new DocumentCollection { Id = Settings.PharmaciesCollectionId }, new RequestOptions { OfferThroughput = 1000 });
            var query_pharmacies = client_document.CreateDocumentQuery<Domain.Pharmacy>(collection_pharmacies.Resource.SelfLink, new SqlQuerySpec()
            {
                QueryText = "SELECT * FROM Pharmacies"
            });
            pharmacies = query_pharmacies.ToList();

            List<Domain.ProductAvailability> productAvailability = null;
            var collection_productAvailability = await client_document.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(Settings.DatabaseId), new DocumentCollection { Id = Settings.ProductAvailabilityCollectionId }, new RequestOptions { OfferThroughput = 1000 });
            var query_productAvailability = client_document.CreateDocumentQuery<Domain.ProductAvailability>(collection_productAvailability.Resource.SelfLink, new SqlQuerySpec()
            {
                QueryText = $"SELECT * FROM ProductAvailability"
            });
            productAvailability = query_productAvailability.ToList();

            List<Domain.ProductAvailabilityData> result = new List<Domain.ProductAvailabilityData>();

            Parallel.ForEach(productAvailability, pa =>
            {
                if (products.Exists(x => x.Id == pa.ProductId))
                {
                    ProductAvailabilityData pad = new ProductAvailabilityData();
                    pad.PharmacyName = pharmacies.Find(x => x.Id == pa.PharmacyId).Name;
                    pad.ProductName = products.Find(x => x.Id == pa.ProductId).Name;
                    pad.ProductImage = products.Find(x => x.Id == pa.ProductId).ImageUrl;
                    pad.Presentation = products.Find(x => x.Id == pa.ProductId).Presentation;
                    pad.Quantity = pa.Quantity;
                    result.Add(pad);
                }
            });

            // Fetching the name from the path parameter in the request URL
            return req.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}