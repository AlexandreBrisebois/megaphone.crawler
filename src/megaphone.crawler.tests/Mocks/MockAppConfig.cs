namespace Megaphone.Crawler.Tests.Mocks
{

    public class MockAppConfig : AppConfig
    {
        public MockAppConfig(bool resourcePush, string resourceApiUrl)
        {
            Set("resourcePush", resourcePush);
            Set("resourceApiUrl", resourceApiUrl);
        }
    }
}