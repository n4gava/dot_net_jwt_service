using JwtService.Entities.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace JwtService.Entities
{
    public class UserToken : IEntity
    {
        [Key]
        public string Token { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public DateTimeOffset CreatedOn { get; set; }

        [Required]
        public long ExpirationMinutes { get; set; }
    }
}
