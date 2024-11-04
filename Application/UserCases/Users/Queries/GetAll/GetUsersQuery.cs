using MediatR;

namespace Application.UserCases.Users.Queries.GetAll
{
    public class GetUsersQuery : IRequest<GetUsersResponse>;
}
