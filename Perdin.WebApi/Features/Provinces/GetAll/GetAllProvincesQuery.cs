using MediatR;

namespace Perdin.WebApi.Features.Provinces.GetAll;

public class GetAllProvincesQuery : IRequest<List<GetAllProvincesResponse>>
{
}
