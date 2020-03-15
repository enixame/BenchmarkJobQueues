using System;

namespace BenchMarkQueue.Queues
{
    public interface IDispatcherQueue : IDisposable
    {
        void Enqueue(Action job);
    }
}