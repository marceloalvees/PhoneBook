using MediatR;

namespace Application.UserCases.Users.Queries.GetByEmail
{
    public record class GetByEmailQuery(string email) : IRequest<GetByEmailResponse>
    {
    }
}
