namespace K1vs.DotChat.Chats
{
    using System.Collections.Generic;
    using Participants;

    public interface IHasPersonalizedChat<out TPersonalizedChat, out TChatParticipantCollection, out TChatParticipant>
        where TPersonalizedChat : IPersonalizedChat<TChatParticipantCollection, TChatParticipant>
        where TChatParticipantCollection : IReadOnlyCollection<TChatParticipant>
        where TChatParticipant : IChatParticipant
    {
        TPersonalizedChat PersonalizedChat { get; }
    }
}
