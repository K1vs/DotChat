namespace K1vs.DotChat.Demo.Others
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Generators;

    public class InMemoryChatMessageIndexGenerator: IChatMessageIndexGenerator
    {
        private long _index = 0;
        public async Task<long> Generate(Guid chatId)
        {
            await Task.Yield();
            return Interlocked.Increment(ref _index);
        }
    }
}
