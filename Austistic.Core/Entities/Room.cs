using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Austistic.Core.Entities
{
    public class Room: BaseEntity
    {
        public string FriendId { get; set; }
        public string RoomName { get; set; }
        public Friend Friend { get; set; }
        public List<RoomMessages> Messages { get; set; }
    }
}
