using MediatR;

namespace Perdin.WebApi.Features.Countries.GetAll;

public class GetAllCountriesQuery : IRequest<List<GetAllCountriesResponse>>
{
}
