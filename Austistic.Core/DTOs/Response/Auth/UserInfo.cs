using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Austistic.Core.DTOs.Response.Auth
{
    public class UserInfo
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public string UserName { get; set; }
        public bool isSuspended { get; set; }
        public bool ShouldShowOnSearch { get; set; }
        public bool IsTokenCreated { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string ActiveSubcriptionName { get; set; }
        public string ProfilePicture { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public bool isSubActive { get; set; }
        public DateTime Created { get; set; }
        public List<string> AccessibleModule { get; set; }
    }
}
