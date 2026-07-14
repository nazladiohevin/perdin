using MediatR;

namespace Perdin.WebApi.Features.BusinessTripRequests.Delete;

public class DeleteBusinessTripRequestCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public int CurrentUserId { get; set; }
    public bool IsAdminOrSdm { get; set; }
}
