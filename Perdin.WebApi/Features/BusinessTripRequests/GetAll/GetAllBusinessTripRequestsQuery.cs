using MediatR;

namespace Perdin.WebApi.Features.BusinessTripRequests.GetAll;

public class GetAllBusinessTripRequestsQuery : IRequest<List<GetAllBusinessTripRequestsResponse>>
{
    public string? Status { get; set; }
    public string SortBy { get; set; } = "newest";
    public int CurrentUserId { get; set; }
    public bool IsAdminOrSdm { get; set; }
}
