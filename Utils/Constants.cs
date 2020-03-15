
using System;

namespace BenchMarkQueue.Utils
{
    public static class Constants
    {
        private const int DefaultConcurrencyMultiplier = 4;
        public static int DefaultConcurrencyLevel => DefaultConcurrencyMultiplier * Environment.ProcessorCount;

        public const int SegmentSize = 32;
    }
}
