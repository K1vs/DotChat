namespace K1vs.DotChat.Implementations.DependencyCore
{
    using Dependency;
    using Microsoft.Extensions.DependencyInjection;

    public static class DependencyExtensions
    {
        public static IServiceCollection RegisterDotChat(this IServiceCollection serviceCollection, IDependencyModule module, bool rootModule = true)
        {
            module.Register(new DependencyRegistrar(serviceCollection), rootModule);
            return serviceCollection;
        }
    }
}
