namespace DGRC.Models
{
    public class ChangePasswordRequest
    {
        public string UserId { get; set; } = string.Empty; // Or current username
        public string CurrentPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
        public string ConfirmNewPassword { get; set; } = string.Empty;
        public string Otp { get; set; } = string.Empty; // NEW: OTP field for verification
    }

    public class SendOtpForChangePasswordRequest
    {
        public string UserId { get; set; } = string.Empty;
        public string CurrentPassword { get; set; } = string.Empty;
    }
}
