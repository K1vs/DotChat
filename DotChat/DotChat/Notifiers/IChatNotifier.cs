namespace K1vs.DotChat.Notifiers
{
    using System.Collections.Generic;
    using Chats;
    using Events.Chats;
    using Handlers;
    using Participants;

    public interface IChatNotifier<in TChat, in TChatInfo, in TChatParticipantCollection, in TChatParticipant> :
        IHandleEvent<IChatAddedEvent<TChat, TChatParticipantCollection, TChatParticipant>>,
        IHandleEvent<IChatInfoEditedEvent<TChatInfo>>,
        IHandleEvent<IChatRemovedEvent<TChatInfo>>
        where TChat : IChat<TChatParticipantCollection, TChatParticipant>
        where TChatInfo: IChatInfo
        where TChatParticipantCollection : IReadOnlyCollection<TChatParticipant>
        where TChatParticipant : IChatParticipant
    {
    }
}
