namespace K1vs.DotChat.Services
{
    using Configuration;

    public abstract class ServiceBase<TDotChatConfiguration>
        where TDotChatConfiguration : IChatServicesConfiguration
    {
        protected readonly string ServiceName;
        protected readonly TDotChatConfiguration ChatServicesConfiguration;

        protected ServiceBase(TDotChatConfiguration chatServicesConfiguration)
        {
            ChatServicesConfiguration = chatServicesConfiguration;
            ServiceName = GetType().FullName;
        }
    }
}
