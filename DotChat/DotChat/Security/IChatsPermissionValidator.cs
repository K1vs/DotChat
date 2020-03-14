namespace K1vs.DotChat.Security
{
    using K1vs.DotChat.Chats;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using Common;
    using Common.Filters;
    using Common.Paging;
    using Participants;
    using K1vs.DotChat.Messages;
    using K1vs.DotChat.Messages.Typed;

    public interface IChatsPermissionValidator<in TPersonalizedChatCollection, in TPersonalizedChat, in TChat, in TChatInfo, in TChatParticipantCollection, in TChatParticipant, in TChatUser, in TChatMessageInfo, in TTextMessage, in TQuoteMessage, in TMessageAttachmentCollection, in TMessageAttachment, in TChatRefMessageCollection, in TChatRefMessage, in TContactMessageCollection, in TContactMessage, in TChatFilter, TChatUserFilter, TMessageFilter, in TPagedResult, in TPagingOptions>
        where TPersonalizedChatCollection: IReadOnlyCollection<TPersonalizedChat>
        where TPersonalizedChat : IPersonalizedChat<TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
        where TChat : IChat<TChatInfo, TChatParticipantCollection, TChatParticipant, TChatUser, TChatMessageInfo, TTextMessage, TQuoteMessage, TMessageAttachmentCollection, TMessageAttachment, TChatRefMessageCollection, TChatRefMessage, TContactMessageCollection, TContactMessage>
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
        where TChatFilter: IChatFilter<TChatUserFilter, TMessageFilter>
        where TChatUserFilter : IChatUserFilter
        where TMessageFilter : IMessageFilter
        where TPagedResult : IPagedResult<TPersonalizedChatCollection, TPersonalizedChat>
        where TPagingOptions : IPagingOptions
    {
        Task ValidateGetSummary(Guid currentUserId, string serviceName, [CallerMemberName] string methodName = null);

        Task ValidateGetPage(Guid currentUserId, TPagedResult chatsPage, TChatFilter filter, TPagingOptions pagingOptions, string serviceName, [CallerMemberName] string methodName = null);
        Task ValidateGetPage(Guid currentUserId, TPagedResult chatsPage, TPagingOptions pagingOptions, string serviceName, [CallerMemberName] string methodName = null);

        Task ValidateGet(Guid currentUserId, TPersonalizedChat chat, string serviceName, [CallerMemberName] string methodName = null);
        Task ValidateAdd(Guid currentUserId, Guid chatId, TChatInfo chatInfo, string serviceName, [CallerMemberName] string methodName = null);
        Task ValidateEditInfo(Guid currentUserId, Guid chatId, TChatInfo chatInfo, string serviceName, [CallerMemberName] string methodName = null);
        Task ValidateRemove(Guid currentUserId, Guid chatId, string serviceName, [CallerMemberName] string methodName = null);
    }
}
