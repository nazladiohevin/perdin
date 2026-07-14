namespace Perdin.WebApi.Features.Cities.GetAll;

public class GetAllCitiesResponse
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
    }
}
