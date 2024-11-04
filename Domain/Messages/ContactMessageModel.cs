using Domain.Entities;

namespace Domain.Messages
{
    public class ContactMessageModel
    {
        public string MessageId { get; set; }
        public Contact Contact { get; set; }
    }
}
