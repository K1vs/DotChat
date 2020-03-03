namespace K1vs.DotChat.Messages
{
    using System;

    [Flags]
    public enum MessageType
    {
        None = 0,
        Text = 1 << 0,
        Quote = 1 << 1,
        Attachment = 1 << 2,
        ChatRef = 1 << 3,
        Contact = 1 << 4,
        Custom = 1 << 16
    }
}
