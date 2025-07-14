namespace DGRC.Models
{
    public class UserProfileResponse
    {
        public string Name { get; set; } = string.Empty;
        public string DistrictName { get; set; } = string.Empty;
        public string BlockName { get; set; } = string.Empty;
        public string HscName { get; set; } = string.Empty;
        // Add any other user details you want to display on the profile screen
    }
}
