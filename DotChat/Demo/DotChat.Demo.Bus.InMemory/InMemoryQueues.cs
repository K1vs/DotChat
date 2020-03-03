namespace K1vs.DotChat.Demo.Bus.InMemory
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class InMemoryQueues
    {
        public readonly BlockingCollection<object> WorkerQueue = new BlockingCollection<object>();

        public readonly BlockingCollection<object> NotificationQueue = new BlockingCollection<object>();
    }
}
