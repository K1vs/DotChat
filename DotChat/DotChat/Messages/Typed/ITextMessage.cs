namespace K1vs.DotChat.Messages.Typed
{
    using Common;

    public interface ITextMessage: ICustomizable
    {
        string Content { get; }
        int? CollapseIndex { get; }
    }
}
