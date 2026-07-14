using MediatR;
using Perdin.WebApi.Features.Cities.GetById;

namespace Perdin.WebApi.Features.Cities.Update;

public class UpdateCityCommand : IRequest<GetCityByIdResponse>
{
    public int Id { get; set; }
    public UpdateCityRequest Request { get; set; } = null!;
}
