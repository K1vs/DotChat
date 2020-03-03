namespace K1vs.DotChat.Messages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IHasTimestamp
    {
        DateTime Timestamp { get; }
    }
}
