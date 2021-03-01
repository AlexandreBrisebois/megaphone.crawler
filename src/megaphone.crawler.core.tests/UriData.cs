using System;
using Xunit;

namespace megaphone.crawler.core.tests
{
    public class UriData : TheoryData<Uri, Guid>
    {
        public UriData()
        {
            Add(new Uri("http://www.google.com"), new Guid("407690c8-8a65-5bb6-a884-cfbc5a8f6d4a"));
            Add(new Uri("https://devblogs.microsoft.com/dotnet/staying-safe-with-dotnet-containers/"), new Guid("d64bc5a5-f2d2-572c-bfbf-b99e5340c0d9"));
            Add(new Uri("https://devblogs.microsoft.com/dotnet/feed/"), new Guid("c50f7543-6b69-5e46-ae5e-bf0b82dede0e"));
        }
    }
}
