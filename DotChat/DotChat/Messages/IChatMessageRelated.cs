namespace K1vs.DotChat.Messages
{
    using System;

    public interface IChatMessageRelated
    {
        Guid MessageId { get; }
    }
}
