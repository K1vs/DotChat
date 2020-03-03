namespace K1vs.DotChat.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IChatServicesConfiguration
    {
        int DefaultChatsPageSize { get; }
        int DefaultChatMessagesPageSize { get; }
    }
}
