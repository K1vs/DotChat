namespace K1vs.DotChat.Implementations.DependencyCore
{
    using System;
    using Dependency;
    using Microsoft.Extensions.DependencyInjection;

    public class DependencyResolver: IDependencyResolver
    {
        private readonly IServiceProvider _serviceProvider;

        public DependencyResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public TService Resolve<TService>()
        {
            return _serviceProvider.GetService<TService>();
        }

        public object Resolve(Type type)
        {
            return _serviceProvider.GetService(type);
        }
    }
}
