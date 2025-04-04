using Austistic.Core.Entities;

namespace Austistic.Core.DTOs.Request.Friend
{
    public class ApproveFriendReq
    {
        public FriendStatus FriendStatus { get; set; }
        public string FriendId { get; set; }
    }
}
