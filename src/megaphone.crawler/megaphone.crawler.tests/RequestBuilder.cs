using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace megaphone.crawler.tests
{
    public class TestHttpResquestData : HttpRequestData
    {
        private ReadOnlyMemory<byte>? body;
        private HttpHeadersCollection headers = new();
        private readonly IReadOnlyCollection<IHttpCookie> cookies = new List<IHttpCookie>().AsReadOnly();
        private readonly Uri url;

        public override ReadOnlyMemory<byte>? Body => this.body;

        public TestHttpResquestData(Uri url, object body)
        {
            this.body = new ReadOnlyMemory<byte>(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(body)));
            this.url = url;
        }

        public override HttpHeadersCollection Headers => this.headers;

        public override IReadOnlyCollection<IHttpCookie> Cookies => this.cookies;

        public override Uri Url => this.url;

        public override IEnumerable<ClaimsIdentity> Identities => new List<ClaimsIdentity>();

        public override string Method => "post";
    }


    public class RequestBuilder
    {
        public static HttpRequestData CreatePostHttpRequest(object body)
        {           
            return new TestHttpResquestData(new Uri("http://localhost:80/crawl"),body);
        }
    }
}
