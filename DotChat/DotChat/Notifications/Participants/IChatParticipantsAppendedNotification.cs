namespace K1vs.DotChat.Notifications.Participants
{
    using System.Collections.Generic;
    using DotChat.Chats;
    using DotChat.Participants;
    using Events;

    public interface IChatParticipantsAppendedNotification<out TParticipationResultCollection, out TParticipationResult, out TChatParticipant> : IChatParticipantsNotification, IChatRelated
        where TParticipationResultCollection : IReadOnlyCollection<TParticipationResult>
        where TParticipationResult : IParticipationResult<TChatParticipant>
        where TChatParticipant : IChatParticipant
    {
        TParticipationResultCollection Added { get; }
        TParticipationResultCollection Invited { get; }
    }
}
