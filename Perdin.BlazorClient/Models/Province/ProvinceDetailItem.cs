namespace Perdin.BlazorClient.Models.Province
{
    public class ProvinceDetailItem
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Island { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public ProvinceDetailCountry? Country { get; set; }
    }

    public class ProvinceDetailCountry
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsForeign { get; set; }
    }
}
