using MediatR;

namespace Application.UserCases.Users.Queries.Get
{
    public class GetUserByIdQuery : IRequest<GetUserByIdResponse>
    {
        public required int Id { get; set; }
    }
}
