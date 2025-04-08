namespace Austistic.Core.Entities
{
    public class SupportMessage : BaseEntity
    {
        public SupportTicket SupportTicket { get; set; }
        public string SupportTicketId { get; set; }
        public string Message { get; set; }
        public bool IsAdmin { get; set; }
    }
}
