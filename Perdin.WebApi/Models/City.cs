namespace Perdin.WebApi.Models
{
    public class City
    {
        public int Id { get; set; }
        public int ProvinceId { get; set; }
        public string Name { get; set; } = null!;
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public Province Province { get; set; } = null!;
        public ICollection<BusinessTripRequest> BusinessTripRequestsAsOrigin { get; set; } = new List<BusinessTripRequest>();
        public ICollection<BusinessTripRequest> BusinessTripRequestsAsDestination { get; set; } = new List<BusinessTripRequest>();
    }
}
