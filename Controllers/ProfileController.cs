using DGRC.Models;
using Microsoft.AspNetCore.Mvc;

namespace DGRC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        // GET: api/Profile/MyProfile
        [HttpGet("MyProfile")]
        public ActionResult<ApiResponse<UserProfileResponse>> GetMyProfile(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return BadRequest(new ApiResponse<UserProfileResponse> { Status = false, Message = "User ID is required." });
            }

            // Simulate fetching user profile data
            var user = AccountController._users.FirstOrDefault(u => u.UserId == userId);
            if (user == null)
            {
                return NotFound(new ApiResponse<UserProfileResponse> { Status = false, Message = "User not found." });
            }

            var profile = new UserProfileResponse
            {
                Name = user.FullName,
                DistrictName = "ARARIA", // Example static data
                BlockName = "Araria",
                HscName = "Araria RS"
            };

            return Ok(new ApiResponse<UserProfileResponse> { Status = true, Message = "User profile fetched successfully.", Data = profile });
        }
    }
}
