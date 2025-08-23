using Microsoft.AspNetCore.Identity;

namespace Austistic.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public bool isSuspended { get; set; } = false;
        public string? ProfilePicture { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public bool ShouldShowOnSearch { get; set; } = true;
        public bool IsTokenCreated { get; set; } = false;
        public string? EncToken { get; set; } 
        public ConfirmEmailToken ConfirmEmailToken { get; set; }
        public ForgetPasswordToken ForgetPasswordToken { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;


    }
}
