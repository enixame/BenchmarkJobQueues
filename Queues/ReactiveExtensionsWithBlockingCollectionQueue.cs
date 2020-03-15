using System;
using System.Collections.Concurrent;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace BenchMarkQueue.Queues
{
    public sealed class ReactiveExtensionsWithBlockingCollectionQueue : IDispatcherQueue
    {
        private readonly IDisposable _subscription;
        private readonly BlockingCollection<Action> _blockingCollectionQueue = new BlockingCollection<Action>();

        public ReactiveExtensionsWithBlockingCollectionQueue()
        {
            IObservable<Action> jobsObservable = _blockingCollectionQueue.
                GetConsumingEnumerable().
                ToObservable(TaskPoolScheduler.Default);

            _subscription = jobsObservable.Subscribe(job =>
                {
                    job.Invoke();
                });
        }

        public void Enqueue(Action job)
        {
            _blockingCollectionQueue.Add(job);
        }

        public void Dispose()
        {
            _subscription.Dispose();
            _blockingCollectionQueue.CompleteAdding();
        }
    }
}
