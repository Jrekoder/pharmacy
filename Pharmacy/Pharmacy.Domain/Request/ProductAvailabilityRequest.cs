namespace Pharmacy.Domain
{
    public class ProductAvailabilityRequest : BaseRequest
    {
        public ProductAvailabilityFilter Filter { get; set; }
    }
}