﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Austistic.Core.Entities
{
    public class ReadMassageCount : BaseEntity
    {
        public string MessageId { get; set; }
        public RoomMessages Message { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public DateTime ReadAt { get; set; } = DateTime.UtcNow;
    }
}
