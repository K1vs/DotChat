namespace K1vs.DotChat.Events.Chats
{
    using System.Collections.Generic;
    using DotChat.Chats;
    using DotChat.Participants;

    public interface IChatAddedEvent<out TChat, out TChatParticipantCollection, out TChatParticipant> : IChatEvent, IHasChat<TChat, TChatParticipantCollection, TChatParticipant>
        where TChat : IChat<TChatParticipantCollection, TChatParticipant>
        where TChatParticipantCollection : IReadOnlyCollection<TChatParticipant>
        where TChatParticipant : IChatParticipant
    {
    }
}
