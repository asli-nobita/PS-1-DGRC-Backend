namespace DGRC.Models
{
    public class ForgotPasswordRequest
    {
        public string UserId { get; set; } = string.Empty; // As per your screen, it's user ID
    }

    public class OtpVerifyRequest
    {
        public string UserId { get; set; } = string.Empty;
        public string Otp { get; set; } = string.Empty;
    }

    public class ResetPasswordRequest
    {
        public string UserId { get; set; } = string.Empty;
        public string Otp { get; set; } = string.Empty; // Often required to ensure valid reset session
        public string NewPassword { get; set; } = string.Empty;
    }
}
