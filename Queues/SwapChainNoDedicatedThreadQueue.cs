using System;
using System.Collections.Generic;
using System.Threading;
using BenchMarkQueue.Utils;

namespace BenchMarkQueue.Queues
{
    public sealed class SwapChainNoDedicatedThreadQueue : IDispatcherQueue
    {
        private static readonly int DefaultCapacity = Constants.SegmentSize;
        private readonly int _capacity;
        private readonly object _syncLock = new object();

        private Queue<Action> _queue;
        private bool _delegateQueuedOrRunning;

        public SwapChainNoDedicatedThreadQueue() 
            : this(DefaultCapacity)
        {
        }

        public SwapChainNoDedicatedThreadQueue(int capacity)
        {
            _capacity = capacity;
            _queue = new Queue<Action>(_capacity);
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

        private void ConsumeQueue(object state)
        {
            while (true)
            {
                lock (_syncLock)
                    if (_queue.Count == 0)
                    {
                        _delegateQueuedOrRunning = false;
                        break;
                    }

                Queue<Action> newQueue = new Queue<Action>(_capacity);
                Queue<Action> actualOldQueue;

                lock (_syncLock)
                {
                    actualOldQueue = _queue;
                    _queue = newQueue;
                }

                while (actualOldQueue?.Count > 0)
                {
                    Action job = actualOldQueue.Dequeue();
                    job?.Invoke();
                }
            }
        }

        public void Dispose()
        {
            // unused
        }
    }
}
