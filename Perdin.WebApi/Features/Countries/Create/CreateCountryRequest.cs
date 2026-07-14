using System.ComponentModel.DataAnnotations;

namespace Perdin.WebApi.Features.Countries.Create;

public class CreateCountryRequest
{
    [Required(ErrorMessage = "Nama negara wajib diisi")]
    public string Name { get; set; } = null!;

    public bool? IsForeign { get; set; }
}
