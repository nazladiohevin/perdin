using MediatR;
using Perdin.WebApi.Features.Cities.GetById;

namespace Perdin.WebApi.Features.Cities.Create;

public class CreateCityCommand : IRequest<GetCityByIdResponse>
{
    public CreateCityRequest Request { get; set; } = null!;
}
