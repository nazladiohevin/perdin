using MediatR;

namespace Perdin.WebApi.Features.Cities.Delete;

public class DeleteCityCommand : IRequest<Unit>
{
    public int Id { get; set; }
}
