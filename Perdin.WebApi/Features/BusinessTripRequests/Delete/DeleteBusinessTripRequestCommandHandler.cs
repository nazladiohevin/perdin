using MediatR;
using Microsoft.EntityFrameworkCore;
using Perdin.WebApi.Data;

namespace Perdin.WebApi.Features.BusinessTripRequests.Delete;

public class DeleteBusinessTripRequestCommandHandler(AppDbContext dbContext)
    : IRequestHandler<DeleteBusinessTripRequestCommand, Unit>
{
    public async Task<Unit> Handle(DeleteBusinessTripRequestCommand request, CancellationToken cancellationToken)
    {
        var tripRequest = await dbContext.BusinessTripRequests
            .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

        if (tripRequest == null)
        {
            throw new BusinessTripRequestException(404, "Pengajuan tidak ditemukan");
        }

        if (!request.IsAdminOrSdm && tripRequest.UserId != request.CurrentUserId)
        {
            throw new BusinessTripRequestException(403, "Anda tidak memiliki akses");
        }

        dbContext.BusinessTripRequests.Remove(tripRequest);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
