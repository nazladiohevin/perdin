namespace Perdin.WebApi.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsForeign { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public ICollection<Province> Provinces { get; set; } = new List<Province>();
        public ICollection<BusinessTripRequest> BusinessTripRequests { get; set; } = new List<BusinessTripRequest>();
    }
}
