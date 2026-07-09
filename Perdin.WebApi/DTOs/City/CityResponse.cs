namespace Perdin.WebApi.DTOs.City
{
    public class CityResponse
    {
        public int Id { get; set; }
        public int ProvinceId { get; set; }
        public string Name { get; set; } = null!;
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
