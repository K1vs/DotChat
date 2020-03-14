namespace K1vs.DotChat.Demo.Others
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Generators;

    public class InMemoryChatMessageIndexGenerator: IChatMessageIndexGenerator
    {
        private class Container
        {
            private long _index = 0; 
            public long Generate()
            {
                return Interlocked.Increment(ref _index);
            }
        }

        private ConcurrentDictionary<Guid, Container> _containers = new ConcurrentDictionary<Guid, Container>();
        public async Task<long> Generate(Guid chatId)
        {
            await Task.Yield();
            var container = _containers.GetOrAdd(chatId, k => new Container());
            return container.Generate();
        }
    }
}
