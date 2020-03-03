namespace K1vs.DotChat.Notifications.Participants
{
    using System.Collections.Generic;
    using DotChat.Chats;
    using DotChat.Participants;
    using Events;

    public interface IChatParticipantsAppendedNotification<out TParticipationResultCollection, TParticipationResult, out TChatParticipant> : INotificationBase, IChatRelated
        where TParticipationResultCollection : IReadOnlyCollection<TParticipationResult>
        where TParticipationResult : IParticipationResult<TChatParticipant>
        where TChatParticipant : IChatParticipant
    {
        TParticipationResultCollection Added { get; }
        TParticipationResultCollection Invited { get; }
    }
}
