using MediatR;

namespace Perdin.WebApi.Features.Users.GetRoles;

public class GetRolesQuery : IRequest<List<GetRolesResponse>>
{
}
