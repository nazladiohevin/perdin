using Perdin.WebApi.DTOs.Country;

namespace Perdin.WebApi.DTOs.Province
{
    public class ProvinceDetailResponse
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string Name { get; set; } = null!;
        public string Island { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        
        public CountryResponse Country { get; set; } = null!;
    }
}
