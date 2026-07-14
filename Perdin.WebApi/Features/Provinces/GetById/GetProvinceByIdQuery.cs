using MediatR;

namespace Perdin.WebApi.Features.Provinces.GetById;

public class GetProvinceByIdQuery : IRequest<GetProvinceByIdResponse>
{
    public int Id { get; set; }
}
