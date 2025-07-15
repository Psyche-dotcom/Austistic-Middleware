namespace Austistic.Core.Entities
{

    public class Friend : BaseEntity
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public string FriendUserId { get; set; }
        public ApplicationUser FriendUser { get; set; }

        public FriendStatus Status { get; set; }
    }

    public enum FriendStatus
    {
        Pending,
        Approved,
        Rejected
    }

}
