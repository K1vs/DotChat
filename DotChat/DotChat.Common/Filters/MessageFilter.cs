namespace K1vs.DotChat.Common.Filters
{
    using System;
    using Messages;

    public class MessageFilter: IMessageFilter
    {
        public MessageFilter()
        {
        }

        public MessageFilter(string search, Guid? authorId, DateTime? @from, DateTime? to, MessageType? messageType, MessageStatus? messageStatus, string messageStyle, string customType)
        {
            Search = search;
            AuthorId = authorId;
            From = @from;
            To = to;
            MessageType = messageType;
            MessageStatus = messageStatus;
            MessageStyle = messageStyle;
            CustomType = customType;
        }

        public string Search { get; set; }
        public Guid? AuthorId { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public MessageType? MessageType { get; set; }
        public MessageStatus? MessageStatus { get; set; }
        public string MessageStyle { get; set; }
        public string CustomType { get; set; }
    }
}
