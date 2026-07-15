namespace Perdin.WebApi.Features.Provinces.GetById;

public class GetProvinceByIdResponse
{
    public int Id { get; set; }
    public int CountryId { get; set; }
    public string Name { get; set; } = null!;
    public string Island { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public CountryInfo? Country { get; set; }

    public class CountryInfo
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsForeign { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
