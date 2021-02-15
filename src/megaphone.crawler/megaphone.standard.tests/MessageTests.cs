using Megaphone.Standard.Messages;
using System;
using Xunit;

namespace Megaphone.Standard.Tests
{

    public class MessageTests
    {
        [Fact]
        public void MessageHasActionName()
        {
            var e = MessageBuilder.NewCommand("crawl-request").Make();
            Assert.Equal("crawl-request", e.Name);
        }

        [Fact]
        public void MessageHasId()
        {
            var e = MessageBuilder.NewCommand("crawl-request").Make();
            Assert.False(String.IsNullOrEmpty(e.Id));
        }

        [Fact]
        public void EventMetadataHasCreatedDateTime()
        {
            var e = MessageBuilder.NewCommand("crawl-request").Make();

            Assert.True(e.Created > DateTime.MinValue);
        }

        [Fact]
        public void EventHasData()
        {
            var e = MessageBuilder.NewCommand("crawl-request")
                                  .WithParameters("uri", "http://www.google.com")
                                  .Make();

            Assert.True(e.Parameters["uri"] == "http://www.google.com");
        }

        [Fact]
        public void EventHasMetadata()
        {
            var e = MessageBuilder.NewCommand("crawl-request")
                                  .WithMetadata("content-type", "rss")
                                  .Make();

            Assert.True(e.Metadata["content-type"] == "rss");
        }
    }
}
