namespace K1vs.DotChat.Implementations.DependencyCore
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Dependency;
    using Microsoft.Extensions.DependencyInjection;

    public class DependencyRegistrationBuilder<TImplementation> : IDependencyRegistrationBuilder<TImplementation>
    {
        protected readonly IServiceCollection ServiceCollection;
        protected readonly TImplementation Value;
        protected readonly Func<IDependencyResolver, TImplementation> Factory;
        protected readonly List<Type> ServiceTypes = new List<Type>();
        protected ServiceLifetime _serviceLifetime;
        protected bool RegisterAsSelf;
        

        public DependencyRegistrationBuilder(IServiceCollection serviceCollection)
        {
            ServiceCollection = serviceCollection;
        }

        public DependencyRegistrationBuilder(IServiceCollection serviceCollection, TImplementation value) : this(serviceCollection)
        {
            Value = value;
        }

        public DependencyRegistrationBuilder(IServiceCollection serviceCollection, Func<IDependencyResolver, TImplementation> factory) : this(serviceCollection)
        {
            Factory = factory;
        }

        public virtual IDependencyRegistrationBuilder<TImplementation> As<TService>()
        {
            ServiceTypes.Add(typeof(TService));
            return this;
        }

        public virtual IDependencyRegistrationBuilder<TImplementation> As(Type type)
        {
            ServiceTypes.Add(type);
            return this;
        }

        public virtual IDependencyRegistrationBuilder<TImplementation> AsSelf()
        {
            RegisterAsSelf = true;
            return this;
        }

        public virtual IDependencyRegistrationBuilder<TImplementation> AsSingleton()
        {
            _serviceLifetime = ServiceLifetime.Singleton;
            return this;
        }

        public virtual IDependencyRegistrationBuilder<TImplementation> AsTransient()
        {
            _serviceLifetime = ServiceLifetime.Transient;
            return this;
        }

        public virtual IDependencyRegistrationBuilder<TImplementation> AsScoped()
        {
            _serviceLifetime = ServiceLifetime.Scoped;
            return this;
        }

        public virtual void Build()
        {
            var selfRegistration = RegisterAsSelf || ServiceTypes.Count > 1;

            if (!selfRegistration && ServiceTypes.Count == 0)
            {
                return;
            }

            var serviceType = selfRegistration ? typeof(TImplementation) : ServiceTypes.Single();

            if (Value != null)
            {
                ServiceCollection.Add(new ServiceDescriptor(serviceType, Value));
            }
            else if (Factory != null)
            {
                ServiceCollection.Add(new ServiceDescriptor(serviceType, (provider) => Factory(new DependencyResolver(provider)), _serviceLifetime));
            }
            else
            {
                ServiceCollection.Add(new ServiceDescriptor(serviceType, typeof(TImplementation), _serviceLifetime));
            }

            if (selfRegistration)
            {
                foreach (var otherServiceType in ServiceTypes)
                {
                    ServiceCollection.Add(new ServiceDescriptor(otherServiceType, (provider) => provider.GetService<TImplementation>(), _serviceLifetime));
                }
            }
        }
    }
}
