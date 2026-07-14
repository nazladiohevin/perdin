using MediatR;
using Perdin.WebApi.Features.Users.GetAll;

namespace Perdin.WebApi.Features.Users.GetById;

public class GetUserByIdQuery : IRequest<GetAllUsersResponse>
{
    public int Id { get; set; }
}
