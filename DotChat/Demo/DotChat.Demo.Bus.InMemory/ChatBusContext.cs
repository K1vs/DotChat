namespace K1vs.DotChat.Demo.Bus.InMemory
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DotChat.Bus;

    public class ChatBusContext: IChatBusContext
    {
        private readonly InMemoryBus _bus;

        public ChatBusContext(InMemoryBus bus)
        {
            _bus = bus;
        }

        public IChatCommandSender CommandSender => _bus;

        public IChatEventPublisher EventPublisher => _bus;
    }
}
