namespace DGRC.Models
{
    public class RegisterRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
    }

    public class RegisterResponse
    {
        public string UserId { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
