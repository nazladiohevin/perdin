namespace Perdin.BlazorClient.Models.City
{
    public class CityDetailItem
    {
        public int Id { get; set; }
        public int ProvinceId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public CityDetailProvince Province { get; set; } = null!;
    }

    public class CityDetailProvince
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
