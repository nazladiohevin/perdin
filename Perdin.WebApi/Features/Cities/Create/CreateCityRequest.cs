using System.ComponentModel.DataAnnotations;

namespace Perdin.WebApi.Features.Cities.Create;

public class CreateCityRequest
{
    [Required(ErrorMessage = "Nama kota wajib diisi")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "ProvinceId wajib diisi")]
    public int? ProvinceId { get; set; }

    [Required(ErrorMessage = "Latitude wajib diisi")]
    public decimal? Latitude { get; set; }

    [Required(ErrorMessage = "Longitude wajib diisi")]
    public decimal? Longitude { get; set; }
}
