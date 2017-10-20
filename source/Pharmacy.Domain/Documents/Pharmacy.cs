using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Domain
{
    public class Pharmacy
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("district")]
        public string District { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        public static string GetRandomName()
        {
            string[] result = { "Farmacia de los Angeles", "Farmacia de los Similares", "Farmacia del Ahorrazo", "Grupo Farmacia MX", "Farmacia Gigante", "Farmacia Express" };
            Random rnd = new Random(DateTime.Now.Millisecond);
            return $"{result[rnd.Next(0, result.Length)]}";
        }

        public static string GetRandomAddress()
        {
            string[] result = { "Av. Revolución no.200", "Av. Revolución no. 19", "Boulevard Adolfo Lopez no. 259", "Avenida Vasco de Quiroga 1700" };
            Random rnd = new Random(DateTime.Now.Millisecond);
            return $"{result[rnd.Next(0, result.Length)]}";
        }

        public static Tuple<double, double> GetRandomLatLong()
        {
            List<Tuple<double, double>> result = new List<Tuple<double, double>>();
            result.Add(new Tuple<double, double>(19.361839, -99.189448));
            result.Add(new Tuple<double, double>(19.31265, - 99.243947));
            result.Add(new Tuple<double, double>(19.195932, -99.81366));
            result.Add(new Tuple<double, double>(19.3482778, -99.2019166666666));
            result.Add(new Tuple<double, double>(19.371329, -99.263297));
            Random rnd = new Random(DateTime.Now.Millisecond);

            Tuple<double, double> selected = result[rnd.Next(0, result.Count)];
        
            return selected;
        }
    }
}
