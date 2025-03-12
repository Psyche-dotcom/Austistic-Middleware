using System.ComponentModel.DataAnnotations;

namespace Austistic.Core.DTOs.Request.Auth
{
    public class SignInModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
