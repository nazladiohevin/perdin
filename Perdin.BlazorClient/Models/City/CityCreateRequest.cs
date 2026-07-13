using System.ComponentModel.DataAnnotations;

namespace Perdin.BlazorClient.Models.City
{
    public class CityCreateRequest
    {
        public string Name { get; set; } = string.Empty;
        public int? ProvinceId { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
    }
}
