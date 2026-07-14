using MediatR;

namespace Perdin.WebApi.Features.Provinces.Delete;

public class DeleteProvinceCommand : IRequest<Unit>
{
    public int Id { get; set; }
}
