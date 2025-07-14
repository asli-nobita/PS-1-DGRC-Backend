using DGRC.Models;
using Microsoft.AspNetCore.Mvc;

namespace DGRC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        // GET: api/Dashboard/Summary
        [HttpGet("Summary")]
        public ActionResult<ApiResponse<DashboardSummaryResponse>> GetDashboardSummary(string userId)
        {
            // In a real application, you would fetch data based on the userId from a database.
            // For demonstration, we return static data.
            if (string.IsNullOrWhiteSpace(userId))
            {
                return BadRequest(new ApiResponse<DashboardSummaryResponse> { Status = false, Message = "User ID is required." });
            }

            // Simulate fetching user's name (e.g., from a user service or database)
            var user = AccountController._users.FirstOrDefault(u => u.UserId == userId);
            string userName = user?.FullName ?? "User";

            var summary = new DashboardSummaryResponse
            {
                TotalClaimSubmitted = 479, // Example data from your screen
                TotalClaimAccepted = 450,
                TotalClaimPending = 29,
                UserName = userName
            };

            return Ok(new ApiResponse<DashboardSummaryResponse> { Status = true, Message = "Dashboard summary fetched successfully.", Data = summary });
        }
    }

}
