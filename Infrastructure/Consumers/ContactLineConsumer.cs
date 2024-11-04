using Domain.Abstractions;
using Domain.Messages;
using MassTransit;

namespace Infrastructure.Consumers
{
    public class ContactLineConsumer : IConsumer<ContactMessageModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        public ContactLineConsumer(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Consume(ConsumeContext<ContactMessageModel> context)
        {
            if (_unitOfWork.ContactRepository.GetByUserIdAndEmail(context.Message.Contact.UserId, context.Message.Contact.Email, context.CancellationToken) != null)
            {
                await Task.FromResult(context.Message);
            }

            await _unitOfWork.ContactRepository.AddAsync(context.Message.Contact, context.CancellationToken);
            await _unitOfWork.CommitAsync();
        }
    }
}
