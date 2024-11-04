
using Application.Dto;
using Domain.Entities;
using Domain.Messages;
using MassTransit;
using MediatR;

namespace Application.UserCases.Agenda.Commands.Save
{
    public class SaveContactHandler : IRequestHandler<SaveContactCommands, MessageDto>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        public SaveContactHandler(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task<MessageDto> Handle(SaveContactCommands commands, CancellationToken cancellationToken)
        {
            foreach (var contact in commands.Contacts)
            {
                var contactEntity = new Contact(contact.UserId, contact.Name, contact.Email, contact.Phone);
                var message = new ContactMessageModel
                {
                    MessageId = Guid.NewGuid().ToString(),
                    Contact = contactEntity
                };
                await _publishEndpoint.Publish(message, cancellationToken);
            }

            return new MessageDto(true, "Contacts saved successfully");

        }
    }
}
