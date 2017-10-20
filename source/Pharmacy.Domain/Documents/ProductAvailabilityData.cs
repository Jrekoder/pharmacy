using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Domain
{
    public class ProductAvailabilityData
    {
        [JsonProperty("productname")]
        public string ProductName { get; set; }

        [JsonProperty("presentation")]
        public string Presentation { get; set; }

        [JsonProperty("pharmacyname")]
        public string PharmacyName { get; set; }

        [JsonProperty("productimage")]
        public string ProductImage { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }
    }
}
