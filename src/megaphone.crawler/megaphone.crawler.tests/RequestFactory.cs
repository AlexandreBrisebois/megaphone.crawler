using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Script.Grpc.Messages;
using System.Text.Json;

namespace megaphone.crawler.tests
{

    public class RequestFactory
    {
        public static HttpRequestData CreatePostHttpRequest(object body)
        {
            var request = new HttpRequestData(new RpcHttp()
            {
                Body = new TypedData()
                {
                    String = JsonSerializer.Serialize(body)
                }
            });

            return request;
        }
    }
}
