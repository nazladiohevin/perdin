using MediatR;

namespace Perdin.WebApi.Features.Cities.GetById;

public class GetCityByIdQuery : IRequest<GetCityByIdResponse>
{
    public int Id { get; set; }
}
