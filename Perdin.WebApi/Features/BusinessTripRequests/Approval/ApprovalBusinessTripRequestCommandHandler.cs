using MediatR;
using Microsoft.EntityFrameworkCore;
using Perdin.WebApi.Data;

namespace Perdin.WebApi.Features.BusinessTripRequests.Approval;

public class ApprovalBusinessTripRequestCommandHandler(AppDbContext dbContext)
    : IRequestHandler<ApprovalBusinessTripRequestCommand, Unit>
{
    public async Task<Unit> Handle(ApprovalBusinessTripRequestCommand request, CancellationToken cancellationToken)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);

        var tripRequest = await dbContext.BusinessTripRequests
            .FromSqlInterpolated($"SELECT * FROM business_trip_requests WITH (UPDLOCK) WHERE id = {request.Id}")
            .FirstOrDefaultAsync(cancellationToken);

        if (tripRequest == null)
        {
            throw new BusinessTripRequestException(404, "Pengajuan tidak ditemukan");
        }

        if (tripRequest.Status != "reviewed")
        {
            throw new BusinessTripRequestException(400, "Pengajuan ini sudah diproses sebelumnya.");
        }

        tripRequest.Status = request.Request.Status.ToLowerInvariant();
        tripRequest.ApproverId = request.CurrentUserId;
        tripRequest.ApprovedAt = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);

        return Unit.Value;
    }
}
