using System.ComponentModel.DataAnnotations;

namespace Perdin.WebApi.Features.Provinces.Create;

public class CreateProvinceRequest
{
    [Required(ErrorMessage = "Nama provinsi wajib diisi")]
    public string Name { get; set; } = null!;

    public string? Island { get; set; }

    [Required(ErrorMessage = "CountryId wajib diisi")]
    public int CountryId { get; set; }
}
