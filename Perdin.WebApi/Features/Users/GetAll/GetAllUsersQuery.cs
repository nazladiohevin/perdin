using MediatR;

namespace Perdin.WebApi.Features.Users.GetAll;

public class GetAllUsersQuery : IRequest<List<GetAllUsersResponse>>
{
}
