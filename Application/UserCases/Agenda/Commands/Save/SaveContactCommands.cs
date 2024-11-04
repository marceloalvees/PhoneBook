using Application.Dto;
using MediatR;

namespace Application.UserCases.Agenda.Commands.Save
{
    public class SaveContactCommands : IRequest<MessageDto>
    {
        public List<ContactDto> Contacts { get; set; } = new List<ContactDto>();
    }
}
