namespace SurgeonPortal.Api.Configuration
{
    public class SiteConfiguration
    {
        public string VirtualDirectoryPath { get; set; }

        public SiteConfiguration()
        {
            VirtualDirectoryPath = string.Empty; 
        }
    }
}
