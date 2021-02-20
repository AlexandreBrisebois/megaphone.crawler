using System;

namespace Megaphone.Standard.Time
{
    public class FixedClock : IClock
    {
        public FixedClock(DateTimeOffset now)
        {
            Now = now;
        }
        public DateTimeOffset Now { get; }
    }
}
