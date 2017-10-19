using Newtonsoft.Json;

namespace Pharmacy.Domain
{
    public class ProductAvailability
    {
        [JsonProperty("productid")]
        public string ProductId { get; set; }

        [JsonProperty("pharmacyid")]
        public string PharmacyId { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }
    }
}