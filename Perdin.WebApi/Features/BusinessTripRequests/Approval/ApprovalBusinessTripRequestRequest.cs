using System.ComponentModel.DataAnnotations;

namespace Perdin.WebApi.Features.BusinessTripRequests.Approval;

public class ApprovalBusinessTripRequestRequest
{
    [Required(ErrorMessage = "Status wajib diisi.")]
    [RegularExpression("^(approved|rejected)$", ErrorMessage = "Status hanya boleh berisi 'approved' atau 'rejected'.")]
    public string Status { get; set; } = null!;
}
