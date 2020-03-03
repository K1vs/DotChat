namespace K1vs.DotChat.Messages.Typed
{
    using System;
    using Common;

    public interface IMessageAttachment: ICustomizable
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string Type { get; set; }
    }
}
