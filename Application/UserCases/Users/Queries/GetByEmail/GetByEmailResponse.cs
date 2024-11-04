using Application.Dto;

namespace Application.UserCases.Users.Queries.GetByEmail
{
    public class GetByEmailResponse
    {
        public bool Sucess { get; set; }
        public GetUserDto? User { get; set; }
    }
}
