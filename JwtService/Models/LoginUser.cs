﻿using System.ComponentModel.DataAnnotations;

namespace JwtService.Models
{
    public class LoginUser
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
