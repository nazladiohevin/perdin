using MediatR;
using Microsoft.EntityFrameworkCore;
using Perdin.WebApi.Data;

namespace Perdin.WebApi.Features.BusinessTripRequests.GetAll;

public class GetAllBusinessTripRequestsQueryHandler(AppDbContext dbContext)
    : IRequestHandler<GetAllBusinessTripRequestsQuery, List<GetAllBusinessTripRequestsResponse>>
{
    public async Task<List<GetAllBusinessTripRequestsResponse>> Handle(GetAllBusinessTripRequestsQuery request, CancellationToken cancellationToken)
    {
        var query = dbContext.BusinessTripRequests
            .Include(r => r.User)
            .Include(r => r.OriginCity)
            .Include(r => r.DestinationCity)
            .Include(r => r.DestinationCountry)
            .AsQueryable();

        if (!request.IsAdminOrSdm)
        {
            query = query.Where(r => r.UserId == request.CurrentUserId);
        }

        if (!string.IsNullOrWhiteSpace(request.Status))
        {
            var allowedStatuses = new[] { "reviewed", "rejected", "approved" };
            var requestedStatuses = request.Status
                .ToLowerInvariant()
                .Split(',')
                .Select(s => s.Trim())
                .Where(s => allowedStatuses.Contains(s))
                .ToList();

            if (requestedStatuses.Count > 0)
            {
                query = query.Where(r => requestedStatuses.Contains(r.Status));
            }
        }

        query = request.SortBy.Equals("oldest", StringComparison.InvariantCultureIgnoreCase)
            ? query.OrderBy(r => r.CreatedAt)
            : query.OrderByDescending(r => r.CreatedAt);

        return await query.Select(r => new GetAllBusinessTripRequestsResponse
        {
            Id = r.Id,
            RequestNumber = r.RequestNumber,
            UserName = request.IsAdminOrSdm ? r.User.Name : null,
            DepartureDate = r.DepartureDate,
            ReturnDate = r.ReturnDate,
            OriginCity = r.OriginCity.Name,
            DestinationCity = r.DestinationCity != null ? r.DestinationCity.Name : null,
            DestinationCountry = r.DestinationCountry.Name,
            Status = r.Status,
            IsForeign = r.DestinationCountry.IsForeign,
            CreatedAt = r.CreatedAt,
            UpdatedAt = r.UpdatedAt
        }).ToListAsync(cancellationToken);
    }
}
