using System;

namespace Megaphone.Standard.Time
{
    public interface IClock
    {
        DateTimeOffset Now { get; }
    }
}
