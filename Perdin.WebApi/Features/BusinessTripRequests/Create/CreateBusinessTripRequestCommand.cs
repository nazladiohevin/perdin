using MediatR;

namespace Perdin.WebApi.Features.BusinessTripRequests.Create;

public class CreateBusinessTripRequestCommand : IRequest<Unit>
{
    public CreateBusinessTripRequestRequest Request { get; set; } = null!;
    public int CurrentUserId { get; set; }
    public bool IsAdminOrSdm { get; set; }
}
