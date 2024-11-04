using Application.Dto;
using System.Numerics;

namespace Application.UserCases.Users.Queries.Get
{
    public class GetUserByIdResponse
    {
        public bool Sucess { get; set; }
        public GetUserDto? User { get; set; }
    }
}
