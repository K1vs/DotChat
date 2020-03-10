namespace K1vs.DotChat.Implementations.SignalR
{
    using Microsoft.AspNet.SignalR.Infrastructure;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IConnectionManagerAccessor
    {
        IConnectionManager ConnectionManager { get; }
    }
}
