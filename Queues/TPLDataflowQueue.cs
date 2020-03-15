using System;
using System.Threading.Tasks.Dataflow;

namespace BenchMarkQueue.Queues
{
    public sealed class TPLDataflowQueue : IDispatcherQueue
    {
        private readonly ActionBlock<Action> _actionBlock;

        public TPLDataflowQueue()
        {
            _actionBlock = new ActionBlock<Action>(job =>
            {
                job.Invoke();
            });
        }

        public void Enqueue(Action job)
        {
            _actionBlock.Post(job);
        }

        public void Dispose()
        {
            _actionBlock.Complete();
        }
    }
}
