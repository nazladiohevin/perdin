namespace Perdin.WebApi.Features.Cities.GetById;

public class GetCityByIdResponse
{
    public int Id { get; set; }
    public int ProvinceId { get; set; }
    public string Name { get; set; } = null!;
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public ProvinceInfo? Province { get; set; }

    public class ProvinceInfo
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public CountryInfo? Country { get; set; }
    }

    public class CountryInfo
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsForeign { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
