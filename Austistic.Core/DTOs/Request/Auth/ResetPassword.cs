﻿using System.ComponentModel.DataAnnotations;

namespace Austistic.Core.DTOs.Request.Auth
{
    public class ResetPassword
    {
        [Required]
        public string Password { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
