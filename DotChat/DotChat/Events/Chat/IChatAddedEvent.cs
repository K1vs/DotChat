namespace K1vs.DotChat.Events.Chat
{
    using System.Collections.Generic;
    using Chats;
    using DotChat.Participants;

    public interface IChatAddedEvent<out TChat, out TChatParticipantCollection, out TChatParticipant> : IEventBase, IHasChat<TChat, TChatParticipantCollection, TChatParticipant>
        where TChat : IChat<TChatParticipantCollection, TChatParticipant>
        where TChatParticipantCollection : IReadOnlyCollection<TChatParticipant>
        where TChatParticipant : IChatParticipant
    {
    }
}
