using MediatR;

namespace Perdin.WebApi.Features.Cities.GetAll;

public class GetAllCitiesQuery : IRequest<List<GetAllCitiesResponse>>
{
    public string? Include { get; set; }
}
