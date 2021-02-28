using Megaphone.Standard.Extensions;
using System;
using Xunit;

namespace megaphone.crawler.core.tests
{
    public class DeterministicGuidGeneratedFromUriTests
    {
        public static TheoryData data => new UriData();

        [Theory(DisplayName = "Generate Deterministic Guid From Uri")]
        [MemberData(nameof(data))]
        public void GenerateDeterministicGuidFromUri(Uri uri, Guid expectedGuid)
        {
            Guid guid = uri.ToGuid();
            Assert.Equal(expectedGuid, guid);
        }
    }
}
