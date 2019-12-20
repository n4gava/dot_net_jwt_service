using System.ComponentModel.DataAnnotations;

namespace JwtService.Models
{
    public class RegisterUser
    {
        [Required]
        public string Email { get; set;}

        [Required]
        public string Password { get; set; }
    }
}
