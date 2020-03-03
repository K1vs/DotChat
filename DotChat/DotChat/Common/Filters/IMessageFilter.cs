namespace K1vs.DotChat.Common.Filters
{
    using System;
    using System.Collections.Generic;
    using Messages;

    public interface IMessageFilter
    {
        string Search { get; }
        Guid? AuthorId { get; }
        DateTime? From { get; }
        DateTime? To { get; }
        MessageType? MessageType { get; }
        MessageStatus? MessageStatus { get; }
        string MessageStyle { get; }
        string CustomType { get; }
    }
}
