using Megaphone.Standard.Time;
using System;

namespace Megaphone.Standard.Services
{
    public sealed class TimeService
    {
        private readonly IClock clock;

        public TimeService()
        {
            clock = new UtcClock();
        }
        public TimeService(DateTimeOffset dateTimeOffset)
        {
            clock = new FixedClock(dateTimeOffset);
        }

        public IClock Now => clock;
    }
}
