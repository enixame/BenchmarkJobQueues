using System;
using System.Collections.Concurrent;
using System.Threading;

namespace BenchMarkQueue.Queues
{
    public sealed class BlockingCollectionDedicatedThreadQueue : IDispatcherQueue
    {
        private readonly BlockingCollection<Action> _blockingCollectionQueue = new BlockingCollection<Action>();

        public BlockingCollectionDedicatedThreadQueue()
        {
            var thread = new Thread(ConsumeQueue) { IsBackground = true };
            thread.Start();
        }

        public void Enqueue(Action job)
        {
            _blockingCollectionQueue.Add(job);
        }

        private void ConsumeQueue()
        {
            foreach (var job in _blockingCollectionQueue.GetConsumingEnumerable(CancellationToken.None))
            {
                job.Invoke();
            }
        }

        public void Dispose()
        {
            _blockingCollectionQueue?.CompleteAdding();
        }
    }
}
