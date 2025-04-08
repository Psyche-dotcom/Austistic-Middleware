namespace Austistic.Core.Entities
{
    public class SupportTicket : BaseEntity
    {
        public string TicketNumber { get; set; }
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public string Status { get; set; } = "Open";
        public List<SupportMessage> SupportMessages { get; set; }
    }
}
