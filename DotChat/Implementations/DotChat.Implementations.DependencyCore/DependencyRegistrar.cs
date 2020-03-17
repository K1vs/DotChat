namespace K1vs.DotChat.Implementations.DependencyCore
{
    using System;
    using Dependency;
    using Microsoft.Extensions.DependencyInjection;

    public class DependencyRegistrar: IDependencyRegistrar
    {
        private readonly IServiceCollection ServiceCollection;

        public DependencyRegistrar(IServiceCollection serviceCollection)
        {
            ServiceCollection = serviceCollection;
        }

        public IDependencyRegistrationBuilder<TImplementation> Register<TImplementation>()
        {
            return new DependencyRegistrationBuilder<TImplementation>(ServiceCollection);
        }

        public IDependencyRegistrationBuilder<TImplementation> Register<TImplementation>(TImplementation implementation)
            where TImplementation : class
        {
            return new DependencyRegistrationBuilder<TImplementation>(ServiceCollection, implementation);
        }

        public IDependencyRegistrationBuilder<TImplementation> Register<TImplementation>(Func<IDependencyResolver, TImplementation> factory)
        {
            return new DependencyRegistrationBuilder<TImplementation>(ServiceCollection, factory);
        }
    }
}
