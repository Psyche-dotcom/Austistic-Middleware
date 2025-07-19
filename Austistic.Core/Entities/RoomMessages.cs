namespace Austistic.Core.Entities
{
    public class RoomMessages : BaseEntity
    {
        public Room Room { get; set; }
        public string RoomId { get; set; }
        public string Message { get; set; }
        public string DisplayMessage { get; set; }
        public string MessageType { get; set; }
        public string SentById { get; set; }
        public ApplicationUser SentBy { get; set; }
        public List<ReadMassageCount> ReadCount { get; set; }
    }
}
