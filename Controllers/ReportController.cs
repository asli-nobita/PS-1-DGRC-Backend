using DGRC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DGRC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        // GET: api/Report/ClaimSubmitted
        [HttpGet("ClaimSubmitted")]
        public ActionResult<ApiResponse<ClaimReportResponse>> GetClaimSubmittedReport(string userId, string month, string year)
        {
            // In a real application, filter data by userId, month, and year from a database.
            // For demonstration, we return static data.
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(month) || string.IsNullOrWhiteSpace(year))
            {
                return BadRequest(new ApiResponse<ClaimReportResponse> { Status = false, Message = "User ID, month, and year are required." });
            }

            var reportItem = new ClaimReportItem
            {
                Numerator = "479",
                Denominator = "500",
                Source = "RCH Portal",
                Month = month,
                Year = year,
                WorkPercentage = "95%",
                FinalAmount = "₹475.00",
                FinalAmountAnm = "₹89.00",
                FinalAmountAsha = "₹58.00",
                FinalSubmitStatus = "Final Submit Done Testing"
            };

            var response = new ClaimReportResponse
            {
                Claims = new List<ClaimReportItem> { reportItem }
            };

            return Ok(new ApiResponse<ClaimReportResponse> { Status = true, Message = "Claim submitted report fetched successfully.", Data = response });
        }

        // GET: api/Report/SubmittedActivityDetails/{activityId}
        // This endpoint would fetch detailed information for a specific submitted activity.
        [HttpGet("SubmittedActivityDetails/{activityId}")]
        public ActionResult<ApiResponse<ClaimReportItem>> GetSubmittedActivityDetails(string userId, string activityId)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(activityId))
            {
                return BadRequest(new ApiResponse<ClaimReportItem> { Status = false, Message = "User ID and Activity ID are required." });
            }

            // Simulate fetching specific activity details.
            // This example reuses ClaimReportItem for simplicity, but you might have a more detailed model.
            var details = new ClaimReportItem
            {
                Numerator = "479",
                Denominator = "500",
                Source = "RCH Portal",
                Month = "April",
                Year = "2024-2025",
                WorkPercentage = "95%",
                FinalAmount = "₹475.00",
                FinalAmountAnm = "₹89.00",
                FinalAmountAsha = "₹58.00",
                FinalSubmitStatus = "Final Submit Done Testing"
                // Add more fields as per your "Submitted Details" screen (Screen 7)
                // e.g., ActivityDescription, ForReviewStatus etc.
            };

            return Ok(new ApiResponse<ClaimReportItem> { Status = true, Message = $"Details for activity {activityId} fetched.", Data = details });
        }
    }
}