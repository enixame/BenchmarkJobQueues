using System.Threading;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using BenchMarkQueue.Queues;

namespace BenchMarkQueue
{
    [RankColumn, Orderer(SummaryOrderPolicy.FastestToSlowest), MemoryDiagnoser, EvaluateOverhead, MinColumn, MaxColumn]
    public class MediumExecutionTimeDispatcherQueueBenchmark
    {
        private readonly AutoResetEvent _autoResetEvent;

        [Params(1000)]
        public int JobCounts;

        [Params(1, 10, 100, 1000)]
        public int CpuOperationsPerJob;

        public MediumExecutionTimeDispatcherQueueBenchmark()
        {
            _autoResetEvent = new AutoResetEvent(false);
        }

        [Benchmark]
        public void BlockingCollectionDedicatedThreadQueue()
        {
            DoJobs(new BlockingCollectionDedicatedThreadQueue());
        }

        [Benchmark]
        public void NoDedicatedThreadQueue()
        {
            DoJobs(new NoDedicatedThreadQueue());
        }

        [Benchmark]
        public void SwapChainNoDedicatedThreadQueue()
        {
            DoJobs(new SwapChainNoDedicatedThreadQueue());
        }

        [Benchmark]
        public void ReactiveExtensionsQueue()
        {
            DoJobs(new ReactiveExtensionsQueue());
        }

        [Benchmark]
        public void ReactiveExtensionsWithBlockingCollectionQueue()
        {
            DoJobs(new ReactiveExtensionsWithBlockingCollectionQueue());
        }

        [Benchmark]
        public void TPLDataflowQueue()
        {
            DoJobs(new TPLDataflowQueue());
        }

        [Benchmark]
        public void ChannelQueue()
        {
            DoJobs(new ChannelQueue());
        }

        private void DoJobs(IDispatcherQueue dispatcherQueue)
        {
            for (int i = 0; i < JobCounts - 1; i++)
            {
                dispatcherQueue.Enqueue(() =>
                {
                    Thread.SpinWait(CpuOperationsPerJob);
                });
            }
            dispatcherQueue.Enqueue(() =>
            {
                Thread.SpinWait(CpuOperationsPerJob);
                _autoResetEvent.Set();
            });
            _autoResetEvent.WaitOne();
            dispatcherQueue.Dispose();
        }
    }
}
