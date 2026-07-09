using System.ComponentModel.DataAnnotations;

namespace Perdin.WebApi.DTOs.Country
{
    public class CountryCreateRequest
    {
        [Required(ErrorMessage = "Nama negara wajib diisi.")]
        [MinLength(3, ErrorMessage = "Nama negara minimal 3 karakter.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Status luar negeri wajib diisi.")]
        public bool? IsForeign { get; set; }
    }
}
