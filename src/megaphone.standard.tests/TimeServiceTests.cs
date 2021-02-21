using Megaphone.Standard.Services;
using Megaphone.Standard.Time;
using System;
using Xunit;

namespace Megaphone.Standard.Tests
{
    public class TimeServiceTests
    {
        [Fact]
        public void GetUtcClockFromTimeService()
        {
            var service = new TimeService();
            var clock = service.Now;

            Assert.IsType<UtcClock>(clock);
        }

        [Fact]
        public void GetFixedClockFromTimeService()
        {
            var service = new TimeService(new DateTimeOffset(2021,02,15,1,0,0,TimeSpan.Zero));
            var clock = service.Now;

            Assert.IsType<FixedClock>(clock);
        }
    }
}