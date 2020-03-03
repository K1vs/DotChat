namespace K1vs.DotChat.EventBuilders
{
    using System;
    using System.Collections.Generic;
    using Chats;
    using Events.Chat;
    using Participants;

    public interface IChatsEventBuilder<TChat, TChatInfo, out TChatParticipantCollection, out TChatParticipant>
        where TChat : IChat<TChatParticipantCollection, TChatParticipant>
        where TChatInfo: IChatInfo
        where TChatParticipantCollection : IReadOnlyCollection<TChatParticipant>
        where TChatParticipant : IChatParticipant
    {
        IChatAddedEvent<TChat, TChatParticipantCollection, TChatParticipant> BuildChatAddedEvent(Guid initiatorUserId, TChat chat);
        IChatInfoEditedEvent<TChatInfo> BuildChatInfoEditedEvent(Guid initiatorUserId, Guid chatId, TChatInfo chatInfo);
        IChatRemovedEvent<TChatInfo> BuildChatRemovedEvent(Guid initiatorUserId, Guid chatId, TChatInfo chatInfo);
    }
}
