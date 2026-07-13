using System.ComponentModel.DataAnnotations;

namespace Perdin.BlazorClient.Models.Province
{
    public class ProvinceUpdateRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Island { get; set; } = string.Empty;
        public int? CountryId { get; set; }
    }
}
