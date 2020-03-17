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
        protected readonly IRegistrationBuilder<TImplementation, TActivatorData, TRegistrationStyle> RegistrationBuilder;

        public DependencyRegistrationBuilder(IRegistrationBuilder<TImplementation, TActivatorData, TRegistrationStyle> registrationBuilder)
        {
            RegistrationBuilder = registrationBuilder;
        }

        public virtual IDependencyRegistrationBuilder<TImplementation> As<TService>()
        {
            RegistrationBuilder.As<TService>();
            return this;
        }

        public virtual IDependencyRegistrationBuilder<TImplementation> As(Type type)
        {
            RegistrationBuilder.As(type);
            return this;
        }

        public virtual IDependencyRegistrationBuilder<TImplementation> AsScoped()
        {
            RegistrationBuilder.InstancePerLifetimeScope();
            return this;
        }

        public virtual IDependencyRegistrationBuilder<TImplementation> AsSelf()
        {
            RegistrationBuilder.As<TImplementation>();
            return this;
        }

        public virtual IDependencyRegistrationBuilder<TImplementation> AsSingleton()
        {
            RegistrationBuilder.SingleInstance();
            return this;
        }

        public virtual IDependencyRegistrationBuilder<TImplementation> AsTransient()
        {
            RegistrationBuilder.InstancePerDependency();
            return this;
        }

        public virtual void Build()
        {
        }
    }
}