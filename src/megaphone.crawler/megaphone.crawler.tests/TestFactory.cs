using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace megaphone.crawler.tests
{
    public class TestFactory
    {
        public static DefaultHttpRequest CreateHttpRequest(object body)
        {
            Stream bodyStream = new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(body)));

            var request = new DefaultHttpRequest(new DefaultHttpContext())
            {
                Method = "post",
                ContentType = "application/json",
                Body = bodyStream
            };
            return request;
        }

        public static ILogger CreateLogger(LoggerTypes type = LoggerTypes.Null)
        {
            ILogger logger;

            if (type == LoggerTypes.List)
            {
                logger = new ListLogger();
            }
            else
            {
                logger = NullLoggerFactory.Instance.CreateLogger("Null Logger");
            }

            return logger;
        }
    }
}
