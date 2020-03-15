using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace BenchMarkQueue.Queues
{
    public sealed class ReactiveExtensionsQueue : IDispatcherQueue
    {
        private readonly IDisposable _subscription;
        private readonly Subject<Action> _subject = new Subject<Action>();

        public ReactiveExtensionsQueue()
        {
            _subscription = _subject.ObserveOn(Scheduler.Default)
               .Subscribe(job =>
                {
                    job.Invoke();
                });
        }

        public void Enqueue(Action job)
        {
            _subject.OnNext(job);
        }

        public void Dispose()
        {
            _subscription?.Dispose();
            _subject?.Dispose();
        }
    }
}
