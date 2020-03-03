namespace K1vs.DotChat.Implementations.DependencyCore
{
    using System;
    using Dependency;
    using Microsoft.Extensions.DependencyInjection;

    public class DependencyRegistrar: IDependencyRegistrar
    {
        private readonly IServiceCollection _serviceCollection;

        public DependencyRegistrar(IServiceCollection serviceCollection)
        {
            _serviceCollection = serviceCollection;
        }

        public IDependencyRegistrationBuilder<TImplementation> Register<TImplementation>()
        {
            return new DependencyRegistrationBuilder<TImplementation>(_serviceCollection);
        }

        public IDependencyRegistrationBuilder<TImplementation> Register<TImplementation>(TImplementation implementation)
        {
            return new DependencyRegistrationBuilder<TImplementation>(_serviceCollection, implementation);
        }

        public IDependencyRegistrationBuilder<TImplementation> Register<TImplementation>(Func<IDependencyResolver, TImplementation> factory)
        {
            return new DependencyRegistrationBuilder<TImplementation>(_serviceCollection, factory);
        }
    }
}
