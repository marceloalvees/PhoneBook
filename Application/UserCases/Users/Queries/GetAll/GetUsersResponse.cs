using Application.Dto;

namespace Application.UserCases.Users.Queries.GetAll
{
    public class GetUsersResponse
    {
        public bool Sucess { get; set; }
        public List<GetUserDto> Users { get; set; } = new List<GetUserDto>();
    }
}