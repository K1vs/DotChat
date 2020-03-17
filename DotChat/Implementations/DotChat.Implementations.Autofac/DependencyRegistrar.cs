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
        protected readonly ContainerBuilder ContainerBuilder;

        public DependencyRegistrar(ContainerBuilder containerBuilder)
        {
            ContainerBuilder = containerBuilder;
        }

        public virtual IDependencyRegistrationBuilder<TImplementation> Register<TImplementation>()
        {
            return new DependencyRegistrationBuilder<TImplementation, ConcreteReflectionActivatorData, SingleRegistrationStyle>(ContainerBuilder.RegisterType<TImplementation>());
        }

        public virtual IDependencyRegistrationBuilder<TImplementation> Register<TImplementation>(TImplementation implementation)
            where TImplementation : class
        {
            return new DependencyRegistrationBuilder<TImplementation, SimpleActivatorData, SingleRegistrationStyle>(ContainerBuilder.RegisterInstance(implementation));
        }

        public virtual IDependencyRegistrationBuilder<TImplementation> Register<TImplementation>(Func<IDependencyResolver, TImplementation> factory)
        {
            return new DependencyRegistrationBuilder<TImplementation, SimpleActivatorData, SingleRegistrationStyle>(ContainerBuilder.Register(c => factory(new DependencyResolver(c))));
        }
    }
}
