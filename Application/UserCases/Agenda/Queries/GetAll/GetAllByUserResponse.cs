using Application.Dto;

namespace Application.UserCases.Agenda.Queries.GetAll
{
    public class GetAllByUserResponse
    {
        public bool Sucess { get; set; }
        public List<GetContactDto> Contacts { get; set; } = new List<GetContactDto>();
    }
}
