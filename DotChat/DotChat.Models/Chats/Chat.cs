namespace K1vs.DotChat.Models.Chats
{
    using System;
    using System.Collections.Generic;
    using DotChat.Chats;
    using DotChat.Participants;
    using K1vs.DotChat.Messages;
    using K1vs.DotChat.Messages.Typed;

    public class Chat<TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage> 
        : ChatInfo, IChat<TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TChatInfo : IChatInfo
        where TChatParticipantCollection : IReadOnlyCollection<TChatParticipant>
        where TChatParticipant : IChatParticipant
        where TChatUser : IChatUser
        where TChatMessageInfo : IChatMessageInfo<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TTextMessage : ITextMessage
        where TQuoteMessage : IQuoteMessage<TChatInfo, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TMessageAttachmentCollection : IReadOnlyCollection<TMessageAttachment>
        where TMessageAttachment : IMessageAttachment
        where TChatRefMessageCollection : IReadOnlyCollection<TChatRefMessage>
        where TChatRefMessage : IChatRefMessage<TChatInfo>
        where TContactMessageCollection : IReadOnlyCollection<TContactMessage>
        where TContactMessage : IContactMessage<TChatUser>
    {
        public Chat()
        {
        }

        public Chat(string name, string description, ChatPrivacyMode privacyMode, long version, Guid chatId, TChatParticipantCollection participants, DateTime lastTimestamp, long topIndex, string style = null, string metadata = null) 
            : base(name, description, privacyMode, version, style, metadata)
        {
            ChatId = chatId;
            Participants = participants;
            LastTimestamp = lastTimestamp;
            TopIndex = topIndex;
        }

        public Chat(IChatInfo chatInfo, Guid chatId, TChatParticipantCollection participants, DateTime lastTimestamp, long topIndex) 
            : this(chatInfo.Name, chatInfo.Description, chatInfo.PrivacyMode, chatInfo.Version, chatId, participants, lastTimestamp, topIndex, chatInfo.Style, chatInfo.Metadata) { }

        public Guid ChatId { get; set; }
        public TChatParticipantCollection Participants { get; set; }
        public DateTime LastTimestamp { get; set; }
        public long TopIndex { get; set; }
}
}
