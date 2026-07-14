using System.ComponentModel.DataAnnotations;

namespace Perdin.WebApi.Features.Countries.Update;

public class UpdateCountryRequest
{
    [Required(ErrorMessage = "Nama negara wajib diisi")]
    public string Name { get; set; } = null!;

    public bool? IsForeign { get; set; }
}
