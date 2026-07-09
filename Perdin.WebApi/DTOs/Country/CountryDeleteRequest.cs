using System.ComponentModel.DataAnnotations;

namespace Perdin.WebApi.DTOs.Country
{
    public class CountryDeleteRequest
    {
        [Required(ErrorMessage = "CountryId wajib disertakan.")]
        public int CountryId { get; set; }
    }
}
