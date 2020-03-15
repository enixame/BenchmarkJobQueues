using System;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace BenchMarkQueue.Queues
{
    public sealed class ChannelQueue : IDispatcherQueue
    {
        private readonly ChannelWriter<Action> _channelWriter;

        public ChannelQueue()
        {
            Channel<Action> channel = Channel.CreateUnbounded<Action>(new UnboundedChannelOptions { SingleReader = true });
            ChannelReader<Action> reader = channel.Reader;
            _channelWriter = channel.Writer;

            Task.Run(async () =>
            {
                while (await reader.WaitToReadAsync())
                {
                    while (reader.TryRead(out Action job))
                    {
                        job.Invoke();
                    }
                }
            });
        }

        public void Enqueue(Action job)
        {
            _channelWriter?.TryWrite(job);
        }

        public void Dispose()
        {
            _channelWriter?.Complete();
        }

    }
}
