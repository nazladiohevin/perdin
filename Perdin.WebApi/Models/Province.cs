namespace Perdin.WebApi.Models
{
    public class Province
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string Name { get; set; } = null!;
        public string Island { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public Country Country { get; set; } = null!;
        public ICollection<City> Cities { get; set; } = new List<City>();
    }
}
