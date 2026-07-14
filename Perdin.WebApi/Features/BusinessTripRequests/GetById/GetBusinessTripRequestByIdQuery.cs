using MediatR;

namespace Perdin.WebApi.Features.BusinessTripRequests.GetById;

public class GetBusinessTripRequestByIdQuery : IRequest<GetBusinessTripRequestByIdResponse>
{
    public int Id { get; set; }
    public int CurrentUserId { get; set; }
    public bool IsAdminOrSdm { get; set; }
}
