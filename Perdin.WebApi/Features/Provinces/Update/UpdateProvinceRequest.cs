using System.ComponentModel.DataAnnotations;

namespace Perdin.WebApi.Features.Provinces.Update;

public class UpdateProvinceRequest
{
    [Required(ErrorMessage = "Nama provinsi wajib diisi")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Nama pulau wajib diisi")]
    public string Island { get; set; } = null!;

    [Required(ErrorMessage = "Negara wajib diisi")]
    public int CountryId { get; set; }
}
