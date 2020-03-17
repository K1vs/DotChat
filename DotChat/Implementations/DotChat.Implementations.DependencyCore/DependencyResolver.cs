namespace K1vs.DotChat.Implementations.DependencyCore
{
    using System;
    using Dependency;
    using Microsoft.Extensions.DependencyInjection;

    public class DependencyResolver: IDependencyResolver
    {
        private readonly IServiceProvider ServiceProvider;

        public DependencyResolver(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public virtual TService Resolve<TService>()
        {
            return ServiceProvider.GetService<TService>();
        }

        public virtual object Resolve(Type type)
        {
            return ServiceProvider.GetService(type);
        }
    }
}
