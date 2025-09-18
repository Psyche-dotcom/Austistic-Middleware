using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Austistic.Core.Entities
{
    public class UserSymbolAttachedProcess : BaseEntity
    {
        public string TaskId { get; set; }
        public string UserId { get; set; } 
        public string Description { get; set; } 
        public string Status { get; set; } 
    }
}
