using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Domain
{
    public class Product
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("presentation")]
        public string Presentation { get; set; }

        [JsonProperty("sustance")]
        public string Sustance { get; set; }

        [JsonProperty("expiration_date")]
        public string ExpirationDate { get; set; }

        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }

        public static string GetRandomName()
        {
            string[] result = { "Benzetacil Combinado", "Symbicort Turbohaler", "Benzetacil L-A", "Benzetacil L-A", "Ventolin", "Posipen" };
            Random rnd = new Random(DateTime.Now.Millisecond);
            return $"{result[rnd.Next(0, result.Length)]}";
        }

        public static string GetRandomPresentation()
        {
            string[] result = { "Inyectable", "Inhalador 160µg", "Tabletas 800mg", "Tabletas 500mg", "Inhalador 100µg" };
            Random rnd = new Random(DateTime.Now.Millisecond);
            return $"{result[rnd.Next(0, result.Length)]}";
        }

        public static string GetRandomSustance()
        {
            string[] result = { "Bencilpenicilina Benzatínica", "Budesonide/Formoterol", "Penicilina G benzatínica", "Magaldrato/Dimeticona", "Salbutamol", "Dicloxacilina" };
            Random rnd = new Random(DateTime.Now.Millisecond);
            return $"{result[rnd.Next(0, result.Length)]}";
        }

        public static string GetRandomExpirationDate()
        {
            string[] result = { "2019-01-21", "2019-11-15", "2018-01-21", "2018-02-11", "2019-07-17", "2018-01-21" };
            Random rnd = new Random(DateTime.Now.Millisecond);
            return $"{result[rnd.Next(0, result.Length)]}";
        }

        public static string GetRandomImageUrl()
        {
            string[] result = { "https://www.superama.com.mx/Content/images/products/img_large/0750221693459L2.jpg", "http://www.fahorro.com/media/catalog/product/cache/1/image/1280x1280/9df78eab33525d08d6e5fb8d27136e95/7/5/7501098602587.JPG", "http://www.interfarma.net/imagenes/imagen/images/9690430_G%281%29.jpg", "http://www.fahorro.com/media/catalog/product/cache/1/image/1280x1280/9df78eab33525d08d6e5fb8d27136e95/7/5/7501092793038_1.jpg", "https://dug3fehy1j4tq.cloudfront.net/images/products/dokteronline-ventolin-418-3-1352465102.jpg", "https://super.walmart.com.mx/images/product-images/img_large/00750107063481L.jpg" };
            Random rnd = new Random(DateTime.Now.Millisecond);
            return $"{result[rnd.Next(0, result.Length)]}";
        }
    }
}