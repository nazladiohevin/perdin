using System.ComponentModel.DataAnnotations;

namespace Perdin.WebApi.DTOs.BusinessTripRequest
{
    public class BusinessTripRequestApprovalRequest
    {
        [Required(ErrorMessage = "Status wajib diisi.")]
        [RegularExpression("^(approved|rejected)$", ErrorMessage = "Status hanya boleh berisi 'approved' atau 'rejected'.")]
        public string Status { get; set; } = null!;

    }
}
