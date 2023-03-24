namespace Dreamers.Ui.Infrastructure
{
    public class PaymobConfiguration
    {
        public string ApiKey { get; set; }
        public string Hmac { get; set; }
        public int LiveIntegrationId { get; set; }
        public int TestIntegrationId { get; set; }

        public bool TestMode { get; set; }

    }
}