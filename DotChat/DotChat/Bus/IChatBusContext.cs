namespace K1vs.DotChat.Bus
{
    public interface IChatBusContext
    {
        IChatCommandSender CommandSender { get; }
        IChatEventPublisher EventPublisher { get; }
    }
}
