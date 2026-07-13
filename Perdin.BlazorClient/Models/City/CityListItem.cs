using Perdin.BlazorClient.Models.Province;

namespace Perdin.BlazorClient.Models.City
{
    public class CityListItem
    {
        public int Id { get; set; }
        public int ProvinceId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public ProvinceListItem? Province { get; set; }
    }
}
