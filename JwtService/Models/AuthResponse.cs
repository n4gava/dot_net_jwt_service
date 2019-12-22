namespace JwtService.Models
{
    public class AuthResponse
    {
        public AuthResponse() { }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Token { get; set; }
    }
}
