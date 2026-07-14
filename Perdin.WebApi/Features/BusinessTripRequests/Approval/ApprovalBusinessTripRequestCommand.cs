using MediatR;

namespace Perdin.WebApi.Features.BusinessTripRequests.Approval;

public class ApprovalBusinessTripRequestCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public ApprovalBusinessTripRequestRequest Request { get; set; } = null!;
    public int CurrentUserId { get; set; }
}
