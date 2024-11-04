using MediatR;

namespace Application.UserCases.Agenda.Queries.GetAll
{
    public record class GetAllByUserQueries(int userId) : IRequest<GetAllByUserResponse>
    {
    }
}
