using Megaphone.Standard.Events;
using System;
using Xunit;

namespace Megaphone.Standard.Tests
{
    public class EventTests
    {
        [Fact]
        public void EventHasName()
        {
            var e = EventBuilder.NewEvent("resource-created").Make();
            Assert.Equal("resource-created", e.Name);
        }

        [Fact]
        public void EventHasId()
        {
            var e = EventBuilder.NewEvent("resource-created").Make();
            Assert.False(String.IsNullOrEmpty(e.Id));
        }

        [Fact]
        public void EventMetadataHasCreatedDateTime()
        {
            var e = EventBuilder.NewEvent("resource-created")
                                .Make();

            Assert.True(e.Created > DateTime.MinValue);
        }
        
        [Fact]
        public void EventHasData()
        {
            var e = EventBuilder.NewEvent("resource-created")
                .WithData("resource", "uri", "http://www.google.com")                
                .Make();

            Assert.True(e.Data["resource"]["uri"]=="http://www.google.com");
        }

        [Fact]
        public void EventHasMetadata()
        {
            var e = EventBuilder.NewEvent("resource-created")
                                .WithMetadata("content-type", "rss")
                                .Make();

            Assert.True(e.Metadata["content-type"] == "rss");
        }
    }
}
