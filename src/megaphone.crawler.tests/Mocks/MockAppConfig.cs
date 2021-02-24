namespace Megaphone.Crawler.Tests.Mocks
{

    public class MockAppConfig : IAppConfig
    {
        public MockAppConfig(bool resourcePush, string resourceApiUrl)
        {            
                ResourcePush = resourcePush;
                ResourceApiUrl = resourceApiUrl;           
        }
        public string ResourceApiUrl { get; set; }

        public bool ResourcePush { get; set; }
    }
}