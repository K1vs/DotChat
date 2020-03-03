namespace K1vs.DotChat.Generators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IChatMessageTimestampGenerator
    {
        Task<DateTime> Generate();
    }
}
