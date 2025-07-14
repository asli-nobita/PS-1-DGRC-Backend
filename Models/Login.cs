using System.ComponentModel.DataAnnotations;

namespace DGRC.Models
{
    public class Login
    {
        public string? Email { get; set; }
        public string? password { get; set; }
    }

    public class LoginRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class LoginResponse
    {
        public string UserId { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty; // In a real app, this would be a JWT
    }

}