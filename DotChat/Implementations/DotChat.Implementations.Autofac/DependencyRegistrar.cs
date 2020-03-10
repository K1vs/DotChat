namespace K1vs.DotChat.Implementations.Autofac
{
    using global::Autofac;
    using global::Autofac.Builder;
    using K1vs.DotChat.Dependency;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class DependencyRegistrar : IDependencyRegistrar
    {
        private readonly ContainerBuilder _containerBuilder;

        public DependencyRegistrar(ContainerBuilder containerBuilder)
        {
            _containerBuilder = containerBuilder;
        }

        public IDependencyRegistrationBuilder<TImplementation> Register<TImplementation>()
        {
            return new DependencyRegistrationBuilder<TImplementation, ConcreteReflectionActivatorData, SingleRegistrationStyle>(_containerBuilder.RegisterType<TImplementation>());
        }

        public IDependencyRegistrationBuilder<TImplementation> Register<TImplementation>(TImplementation implementation)
            where TImplementation : class
        {
            return new DependencyRegistrationBuilder<TImplementation, SimpleActivatorData, SingleRegistrationStyle>(_containerBuilder.RegisterInstance(implementation));
        }

        public IDependencyRegistrationBuilder<TImplementation> Register<TImplementation>(Func<IDependencyResolver, TImplementation> factory)
        {
            return new DependencyRegistrationBuilder<TImplementation, SimpleActivatorData, SingleRegistrationStyle>(_containerBuilder.Register(c => factory(new DependencyResolver(c))));
        }
    }
}
