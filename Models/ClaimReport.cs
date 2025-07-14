namespace DGRC.Models
{
    public class ClaimReportItem
    {
        public string Numerator { get; set; } = string.Empty;
        public string Denominator { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;
        public string Month { get; set; } = string.Empty;
        public string Year { get; set; } = string.Empty;
        public string WorkPercentage { get; set; } = string.Empty;
        public string FinalAmount { get; set; } = string.Empty;
        public string FinalAmountAnm { get; set; } = string.Empty;
        public string FinalAmountAsha { get; set; } = string.Empty;
        public string FinalSubmitStatus { get; set; } = string.Empty;
    }

    public class ClaimReportResponse
    {
        public List<ClaimReportItem> Claims { get; set; } = new List<ClaimReportItem>();
    }
}
