using System.ComponentModel.DataAnnotations;

namespace JwtService.Entities
{
    public class User : BaseEntity
    {
        [Required]
        [MinLength(6)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
