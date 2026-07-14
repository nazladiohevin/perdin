using MediatR;

namespace Perdin.WebApi.Features.Countries.Delete;

public class DeleteCountryCommand : IRequest<Unit>
{
    public int Id { get; set; }
}
