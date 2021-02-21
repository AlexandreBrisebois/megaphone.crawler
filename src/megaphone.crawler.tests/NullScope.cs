using System;

namespace Megaphone.Crawler.Tests
{
    public class NullScope : IDisposable
    {
        public static NullScope Instance { get; } = new NullScope();
           private NullScope() { }
        public void Dispose() { }
    }
}