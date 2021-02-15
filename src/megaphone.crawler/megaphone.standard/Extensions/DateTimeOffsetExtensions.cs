using Megaphone.Standard.Time;
using System;

namespace Megaphone.Standard.Extensions
{
    public static class DateTimeOffsetExtensions
    {
        public static IClock ToFixedClock(this DateTimeOffset dateTimeOffset)
        {
            return new FixedClock(dateTimeOffset);
        }
    }
}
