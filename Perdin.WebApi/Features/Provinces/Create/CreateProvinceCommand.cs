using MediatR;
using Perdin.WebApi.Features.Provinces.GetById;

namespace Perdin.WebApi.Features.Provinces.Create;

public class CreateProvinceCommand : IRequest<GetProvinceByIdResponse>
{
    public CreateProvinceRequest Request { get; set; } = null!;
}
