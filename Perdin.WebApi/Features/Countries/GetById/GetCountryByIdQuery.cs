using MediatR;

namespace Perdin.WebApi.Features.Countries.GetById;

public class GetCountryByIdQuery : IRequest<GetCountryByIdResponse>
{
    public int Id { get; set; }
}
