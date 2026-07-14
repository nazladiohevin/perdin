using MediatR;
using Perdin.WebApi.Features.Countries.GetById;

namespace Perdin.WebApi.Features.Countries.Create;

public class CreateCountryCommand : IRequest<GetCountryByIdResponse>
{
    public CreateCountryRequest Request { get; set; } = null!;
}
