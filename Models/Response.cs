namespace DGRC.Models
{
    public class Response
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public bool status { get; set; }
    }

    public class MonthYear
    {
        public List<DistrictList>? districtlist { get; set; }
        public List<BlockList>? blocklist { get; set; }
        public bool Status { get; set; }
        public int StatusCode { get; set; }
        public string? Message { get; set; }
    }

    public class DistrictList
    {
        public string? DistrictID { get; set; }
        public string? DistrictName { get; set; }

    }
    public class BlockList
    {
        public string? BlockID { get; set; }
        public string? BlockName { get; set; }
        public string? DistrictID { get; set; }



    }
}
