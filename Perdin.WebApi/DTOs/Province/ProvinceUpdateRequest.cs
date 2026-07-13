using System.ComponentModel.DataAnnotations;

namespace Perdin.WebApi.DTOs.Province
{
    public class ProvinceUpdateRequest
    {
        [Required(ErrorMessage = "Nama provinsi wajib diisi.")]
        [MinLength(3, ErrorMessage = "Nama provinsi minimal 3 karakter.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Pulau wajib diisi.")]
        public string Island { get; set; } = null!;

        [Required(ErrorMessage = "CountryId wajib diisi.")]
        public int CountryId { get; set; }
    }
}
