using MediatR;
using Perdin.WebApi.Features.Provinces.GetById;

namespace Perdin.WebApi.Features.Provinces.Update;

public class UpdateProvinceCommand : IRequest<GetProvinceByIdResponse>
{
    public int Id { get; set; }
    public UpdateProvinceRequest Request { get; set; } = null!;
}
