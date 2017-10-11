using System;
using System.Threading;

namespace ConcurrentRandom
{
    public class ConcurrentRandom
    {
        private readonly Random _seedGenerator = new Random();
        private readonly ThreadLocal<Random> _threadLocal;

        public ConcurrentRandom()
        {
            _threadLocal = new ThreadLocal<Random>(() =>
            {
                lock (_seedGenerator)
                {
                    return new Random(_seedGenerator.Next());
                }
            });
        }

        public int Next()
        {
            return _threadLocal.Value.Next();
        }

        public int Next(int maxValue)
        {
            return _threadLocal.Value.Next(maxValue);
        }

        public int Next(int minValue, int maxValue)
        {
            return _threadLocal.Value.Next(minValue, maxValue);
        }

        public void NextBytes(byte[] buffer)
        {
            _threadLocal.Value.NextBytes(buffer);
        }
    }
}
