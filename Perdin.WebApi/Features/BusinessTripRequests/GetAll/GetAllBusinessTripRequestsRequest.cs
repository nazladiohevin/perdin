namespace Perdin.WebApi.Features.BusinessTripRequests.GetAll;

public class GetAllBusinessTripRequestsRequest
{
    public string? Status { get; set; }
    public string SortBy { get; set; } = "newest";
}
