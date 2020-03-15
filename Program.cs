using BenchmarkDotNet.Running;

namespace BenchMarkQueue
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<VeryShortExecutionTimeDispatcherQueueBenchmark>();
            BenchmarkRunner.Run<MediumExecutionTimeDispatcherQueueBenchmark>();
        }
    }
}