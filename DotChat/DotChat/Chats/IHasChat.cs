namespace K1vs.DotChat.Chats
{
    using System.Collections.Generic;
    using Participants;

    public interface IHasChat<out TChat, out TChatParticipantCollection, out TChatParticipant>
        where TChat : IChat<TChatParticipantCollection, TChatParticipant>
        where TChatParticipantCollection : IReadOnlyCollection<TChatParticipant>
        where TChatParticipant : IChatParticipant
    {
        TChat Chat { get; }
    }
}
