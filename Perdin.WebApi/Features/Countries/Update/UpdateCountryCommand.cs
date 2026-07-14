using MediatR;
using Perdin.WebApi.Features.Countries.GetById;

namespace Perdin.WebApi.Features.Countries.Update;

public class UpdateCountryCommand : IRequest<GetCountryByIdResponse>
{
    public int Id { get; set; }
    public UpdateCountryRequest Request { get; set; } = null!;
}
