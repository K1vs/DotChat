namespace K1vs.DotChat.Common.Exceptions
{
    /// <summary>
    /// 0 - 99 999
    /// </summary>
    public enum ErrorEntity
    {
        None = 0,
        Chat = ErrorScope.Chat * 1000,
        ChatInfo = Chat + 10,
        PersonalizedChat = Chat + 20,
        PersonalizedChatsSummary = Chat + 30,

        Participant = ErrorScope.Participant * 1000,
        ParticipantType = Participant + 10,

        User = ErrorScope.User * 1000,

        Message = ErrorScope.Message * 1000,
        MessageInfo = Message + 10,
        MessageText = Message + 15,
        MessageQuote = Message + 20,
        MessageAttachments = Message + 25,
        MessageChatRefs = Message + 30,
        MessageContacts = Message + 35
    }
}