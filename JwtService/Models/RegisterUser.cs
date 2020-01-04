using System.ComponentModel.DataAnnotations;

namespace JwtService.Models
{
    public class RegisterUser
    {
        [Required]
        [EmailAddress]
        public string Email { get; set;}

        [Required]
        public string Password { get; set; }

        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
