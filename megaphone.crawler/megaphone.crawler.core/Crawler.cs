using System;
using System.Threading.Tasks;

namespace Megaphone.Crawler.Core
{
    public abstract class Crawler<T>
    {
        public abstract Task<T> GetResourceAsync(Uri uri);
    }
}
