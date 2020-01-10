using System;
using System.ComponentModel.DataAnnotations;

namespace JwtService.Entities
{
    public class UserToken : BaseEntity
    {
        [Required]
        public string Token { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public DateTimeOffset CreatedOn { get; set; }

        [Required]
        public long ExpirationMinutes { get; set; }
    }
}
