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

    public class DependencyRegistrationBuilder<TImplementation, TActivatorData, TRegistrationStyle> : IDependencyRegistrationBuilder<TImplementation>
    {
        private readonly IRegistrationBuilder<TImplementation, TActivatorData, TRegistrationStyle> _registrationBuilder;

        public DependencyRegistrationBuilder(IRegistrationBuilder<TImplementation, TActivatorData, TRegistrationStyle> registrationBuilder)
        {
            _registrationBuilder = registrationBuilder;
        }

        public IDependencyRegistrationBuilder<TImplementation> As<TService>()
        {
            _registrationBuilder.As<TService>();
            return this;
        }

        public IDependencyRegistrationBuilder<TImplementation> As(Type type)
        {
            _registrationBuilder.As(type);
            return this;
        }

        public IDependencyRegistrationBuilder<TImplementation> AsScoped()
        {
            _registrationBuilder.InstancePerLifetimeScope();
            return this;
        }

        public IDependencyRegistrationBuilder<TImplementation> AsSelf()
        {
            _registrationBuilder.As<TImplementation>();
            return this;
        }

        public IDependencyRegistrationBuilder<TImplementation> AsSingleton()
        {
            _registrationBuilder.SingleInstance();
            return this;
        }

        public IDependencyRegistrationBuilder<TImplementation> AsTransient()
        {
            _registrationBuilder.InstancePerDependency();
            return this;
        }

        public void Build()
        {
        }
    }
}