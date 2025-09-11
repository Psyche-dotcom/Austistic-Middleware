using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Austistic.Core.DTOs.Response.Friend
{
    public class GetAllFriendResp
    {
        public string chatRooomName { get; set; }
        public string UserId { get; set; }
        public string message { get; set; }
        public string name { get; set; }
        public decimal unreadCount { get; set; }
        public string url { get; set; }
        public string time { get; set; }
        public string userStatus { get; set; }


    }
}
