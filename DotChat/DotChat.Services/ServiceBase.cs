namespace K1vs.DotChat.Services
{
    using Configuration;

    public abstract class ServiceBase
    {
        protected readonly string ServiceName;
        protected readonly IChatServicesConfiguration ChatServicesConfiguration;

        protected ServiceBase(IChatServicesConfiguration chatServicesConfiguration)
        {
            ChatServicesConfiguration = chatServicesConfiguration;
            ServiceName = GetType().FullName;
        }
    }
}
