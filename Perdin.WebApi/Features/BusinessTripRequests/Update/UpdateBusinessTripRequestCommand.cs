using MediatR;

namespace Perdin.WebApi.Features.BusinessTripRequests.Update;

public class UpdateBusinessTripRequestCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public UpdateBusinessTripRequestRequest Request { get; set; } = null!;
    public int CurrentUserId { get; set; }
    public bool IsAdminOrSdm { get; set; }
}
