﻿using System.ComponentModel.DataAnnotations;

namespace Austistic.Core.DTOs.Request.Auth
{
    public class SignUp
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }

        public int Age { get; set; }
        public string Gender { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }


    }
}
