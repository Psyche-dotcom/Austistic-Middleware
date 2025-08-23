
using System.ComponentModel.DataAnnotations;

namespace Austistic.Core.DTOs.Request.Auth
{
    public class UpdateUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }

        public string UserName { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
    }
}