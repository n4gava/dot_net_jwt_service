using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JwtService.Entities
{
    public class UserTokens
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
