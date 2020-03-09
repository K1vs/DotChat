namespace K1vs.DotChat.Demo.SignalR.Clients
{
    using System.Collections.Generic;
    using Basic.Chats;
    using Implementations.SignalR;
    using Models.Chats;
    using Models.Participants;

    public interface IChatsClient: IChatsClient<PersonalizedChat, ChatInfo, List<ChatParticipant>, ChatParticipant>
    {
    }
}
