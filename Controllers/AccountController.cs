using Microsoft.AspNetCore.Mvc;
using DGRC.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DGRC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        public static readonly List<LoginResponse> _users = new List<LoginResponse>
        {
            new LoginResponse { UserId = "user123", Username = "testuser", FullName = "Ankit Awasthi", Token = "sample_token_123" },
            new LoginResponse { UserId = "cho001", Username = "cho001", FullName = "CHO User One", Token = "sample_token_cho001" }
        };

        public static readonly Dictionary<string, string> _userPasswords = new Dictionary<string, string>
        {
            { "testuser", "password123" },
            { "cho001", "cho_pass" }
        };

        // In-memory OTP store (for demonstration, not secure for production)
        private static readonly Dictionary<string, string> _otps = new Dictionary<string, string>();


        // POST: api/Account/Login
        [HttpPost("Login")]
        public ActionResult<ApiResponse<LoginResponse>> Login([FromBody] LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(new ApiResponse<LoginResponse> { Status = false, Message = "Username and password are required." });
            }

            if (_userPasswords.TryGetValue(request.Username, out var storedPassword) && storedPassword == request.Password)
            {
                var user = _users.FirstOrDefault(u => u.Username == request.Username);
                if (user != null)
                {
                    return Ok(new ApiResponse<LoginResponse> { Status = true, Message = "Login successful!", Data = user });
                }
            }

            return Unauthorized(new ApiResponse<LoginResponse> { Status = false, Message = "Invalid username or password." });
        }

        // POST: api/Account/Register
        [HttpPost("Register")]
        public ActionResult<ApiResponse<RegisterResponse>> Register([FromBody] RegisterRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password) ||
                string.IsNullOrWhiteSpace(request.FullName) || string.IsNullOrWhiteSpace(request.Email))
            {
                return BadRequest(new ApiResponse<RegisterResponse> { Status = false, Message = "All fields are required for registration." });
            }

            if (_users.Any(u => u.Username.Equals(request.Username, StringComparison.OrdinalIgnoreCase)))
            {
                return Conflict(new ApiResponse<RegisterResponse> { Status = false, Message = "Username already exists." });
            }

            var newUserId = Guid.NewGuid().ToString();
            var newUser = new LoginResponse
            {
                UserId = newUserId,
                Username = request.Username,
                FullName = request.FullName,
                Token = "new_user_token_" + newUserId.Substring(0, 5)
            };

            _users.Add(newUser);
            _userPasswords.Add(request.Username, request.Password);

            return CreatedAtAction(
                nameof(Login),
                new { },
                new ApiResponse<RegisterResponse> { Status = true, Message = "Registration successful!", Data = new RegisterResponse { UserId = newUserId, Message = "User created." } }
            );
        }

        // POST: api/Account/ForgotPassword
        [HttpPost("ForgotPassword")]
        public ActionResult<ApiResponse<string>> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.UserId))
            {
                return BadRequest(new ApiResponse<string> { Status = false, Message = "User ID is required." });
            }

            if (!_users.Any(u => u.UserId.Equals(request.UserId, StringComparison.OrdinalIgnoreCase) || u.Username.Equals(request.UserId, StringComparison.OrdinalIgnoreCase)))
            {
                return NotFound(new ApiResponse<string> { Status = false, Message = "User not found." });
            }

            var otp = new Random().Next(1000, 9999).ToString();
            _otps[request.UserId] = otp;

            Console.WriteLine($"Simulating OTP sent to {request.UserId} for Forgot Password: {otp}");

            return Ok(new ApiResponse<string> { Status = true, Message = $"OTP sent to {request.UserId}. OTP: {otp}" });
        }

        // POST: api/Account/VerifyOtp
        [HttpPost("VerifyOtp")]
        public ActionResult<ApiResponse<string>> VerifyOtp([FromBody] OtpVerifyRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.UserId) || string.IsNullOrWhiteSpace(request.Otp))
            {
                return BadRequest(new ApiResponse<string> { Status = false, Message = "User ID and OTP are required." });
            }

            if (_otps.TryGetValue(request.UserId, out var storedOtp) && storedOtp == request.Otp)
            {
                _otps.Remove(request.UserId); // OTP used, remove it
                return Ok(new ApiResponse<string> { Status = true, Message = "OTP Verified successfully." });
            }
            return BadRequest(new ApiResponse<string> { Status = false, Message = "Invalid OTP or User ID." });
        }

        // POST: api/Account/ResetPassword
        [HttpPost("ResetPassword")]
        public ActionResult<ApiResponse<string>> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.UserId) || string.IsNullOrWhiteSpace(request.NewPassword) || string.IsNullOrWhiteSpace(request.Otp))
            {
                return BadRequest(new ApiResponse<string> { Status = false, Message = "User ID, OTP, and New Password are required." });
            }

            if (!_otps.TryGetValue(request.UserId, out var storedOtp) || storedOtp != request.Otp)
            {
                return BadRequest(new ApiResponse<string> { Status = false, Message = "Invalid OTP or OTP expired. Please try forgot password again." });
            }

            var user = _users.FirstOrDefault(u => u.UserId.Equals(request.UserId, StringComparison.OrdinalIgnoreCase));
            if (user != null)
            {
                _userPasswords[user.Username] = request.NewPassword;
                _otps.Remove(request.UserId);
                return Ok(new ApiResponse<string> { Status = true, Message = "Password reset successfully." });
            }

            return NotFound(new ApiResponse<string> { Status = false, Message = "User not found." });
        }

        // NEW ENDPOINT: POST: api/Account/SendOtpForChangePassword
        [HttpPost("SendOtpForChangePassword")]
        public ActionResult<ApiResponse<string>> SendOtpForChangePassword([FromBody] SendOtpForChangePasswordRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.UserId) || string.IsNullOrWhiteSpace(request.CurrentPassword))
            {
                return BadRequest(new ApiResponse<string> { Status = false, Message = "User ID and Current Password are required." });
            }

            var user = _users.FirstOrDefault(u => u.UserId.Equals(request.UserId, StringComparison.OrdinalIgnoreCase));
            if (user == null)
            {
                return NotFound(new ApiResponse<string> { Status = false, Message = "User not found." });
            }

            // Verify current password before sending OTP
            if (!_userPasswords.TryGetValue(user.Username, out var storedPassword) || storedPassword != request.CurrentPassword)
            {
                return Unauthorized(new ApiResponse<string> { Status = false, Message = "Invalid current password." });
            }

            var otp = new Random().Next(1000, 9999).ToString();
            _otps[request.UserId] = otp; // Store OTP for this user

            Console.WriteLine($"Simulating OTP sent to {request.UserId} for Change Password: {otp}");

            return Ok(new ApiResponse<string> { Status = true, Message = $"OTP sent to {request.UserId}. OTP: {otp}" });
        }


        // MODIFIED ENDPOINT: POST: api/Account/ChangePasswordWithOtp
        [HttpPost("ChangePasswordWithOtp")]
        public ActionResult<ApiResponse<string>> ChangePasswordWithOtp([FromBody] ChangePasswordRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.UserId) || string.IsNullOrWhiteSpace(request.NewPassword) ||
                string.IsNullOrWhiteSpace(request.ConfirmNewPassword) || string.IsNullOrWhiteSpace(request.Otp))
            {
                return BadRequest(new ApiResponse<string> { Status = false, Message = "User ID, New Password, Confirm New Password, and OTP are required." });
            }

            if (request.NewPassword != request.ConfirmNewPassword)
            {
                return BadRequest(new ApiResponse<string> { Status = false, Message = "New password and confirm password do not match." });
            }

            var user = _users.FirstOrDefault(u => u.UserId.Equals(request.UserId, StringComparison.OrdinalIgnoreCase));
            if (user == null)
            {
                return NotFound(new ApiResponse<string> { Status = false, Message = "User not found." });
            }

            // Verify OTP
            if (!_otps.TryGetValue(request.UserId, out var storedOtp) || storedOtp != request.Otp)
            {
                return BadRequest(new ApiResponse<string> { Status = false, Message = "Invalid OTP or OTP expired. Please request a new OTP." });
            }

            // Update password
            _userPasswords[user.Username] = request.NewPassword;
            _otps.Remove(request.UserId); // Invalidate OTP after successful use
            return Ok(new ApiResponse<string> { Status = true, Message = "Password changed successfully." });
        }
    }
}
