namespace DGRC.Models
{
    public class DashboardSummaryResponse
    {
        public int TotalClaimSubmitted { get; set; }
        public int TotalClaimAccepted { get; set; }
        public int TotalClaimPending { get; set; }
        // You can add more properties as per your dashboard design
        public string UserName { get; set; } = string.Empty; // From Dashboard screen 3
    }
}
