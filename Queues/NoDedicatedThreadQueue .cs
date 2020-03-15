using System;
using System.Collections.Generic;
using System.Threading;
using BenchMarkQueue.Utils;

namespace BenchMarkQueue.Queues
{
    public sealed class NoDedicatedThreadQueue : IDispatcherQueue
    {
        private static readonly int DefaultCapacity = Constants.SegmentSize;
        private readonly object _syncLock = new object();
        private readonly Queue<Action> _queue;
        private bool _delegateQueuedOrRunning;

        public NoDedicatedThreadQueue()
            : this(DefaultCapacity)
        {
        }

        public NoDedicatedThreadQueue(int capacity)
        {
            _queue = new Queue<Action>(capacity);
        }

        public void Enqueue(Action job)
        {
            lock (_syncLock)
            {
                _queue.Enqueue(job);
                if (!_delegateQueuedOrRunning)
                {
                    _delegateQueuedOrRunning = true;
                    ThreadPool.UnsafeQueueUserWorkItem(ConsumeQueue, null);
                }
            }
        }

        private void ConsumeQueue(object ignored)
        {
            while (true)
            {
                Action job;
                lock (_syncLock)
                {
                    if (_queue.Count == 0)
                    {
                        _delegateQueuedOrRunning = false;
                        break;
                    }

                    job = _queue.Dequeue();
                }

                job.Invoke();  
            }
        }

        public void Dispose()
        {
            // unused
        }
    }
}
