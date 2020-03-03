namespace K1vs.DotChat.Basic.Generators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DotChat.Generators;

    public class ChatMessageTimestampGenerator: IChatMessageTimestampGenerator
    {
        public Task<DateTime> Generate()
        {
            return Task.FromResult(DateTime.UtcNow);
        }
    }
}
