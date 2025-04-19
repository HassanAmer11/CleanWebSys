namespace ECommerce.WebApi.Service
{
    public class ServiceConfiguration
    {
        public string? Name { get; set; }
        public string? Version { get; set; }
        public string? Title { get; set; }
        public int DefaultRequestTimeOutInMs { get; set; } = 9000;
    }
}
