namespace K1vs.DotChat.Implementations.DependencyCore
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Dependency;
    using Microsoft.Extensions.DependencyInjection;

    public class DependencyRegistrationBuilder<TImplementation> : IDependencyRegistrationBuilder<TImplementation>
    {
        private readonly IServiceCollection _serviceCollection;
        private readonly TImplementation _value;
        private readonly Func<IDependencyResolver, TImplementation> _factory;
        private List<Type> _serviceTypes = new List<Type>();
        private ServiceLifetime _serviceLifetime;
        private bool _asSelf;
        

        public DependencyRegistrationBuilder(IServiceCollection serviceCollection)
        {
            _serviceCollection = serviceCollection;
        }

        public DependencyRegistrationBuilder(IServiceCollection serviceCollection, TImplementation value) : this(serviceCollection)
        {
            _value = value;
        }

        public DependencyRegistrationBuilder(IServiceCollection serviceCollection, Func<IDependencyResolver, TImplementation> factory) : this(serviceCollection)
        {
            _factory = factory;
        }

        public IDependencyRegistrationBuilder<TImplementation> As<TService>()
        {
            _serviceTypes.Add(typeof(TService));
            return this;
        }

        public IDependencyRegistrationBuilder<TImplementation> As(Type type)
        {
            _serviceTypes.Add(type);
            return this;
        }

        public IDependencyRegistrationBuilder<TImplementation> AsSelf()
        {
            _asSelf = true;
            return this;
        }

        public IDependencyRegistrationBuilder<TImplementation> AsSingleton()
        {
            _serviceLifetime = ServiceLifetime.Singleton;
            return this;
        }

        public IDependencyRegistrationBuilder<TImplementation> AsTransient()
        {
            _serviceLifetime = ServiceLifetime.Transient;
            return this;
        }

        public IDependencyRegistrationBuilder<TImplementation> AsScoped()
        {
            _serviceLifetime = ServiceLifetime.Scoped;
            return this;
        }

        public void Build()
        {
            var selfRegistration = _asSelf || _serviceTypes.Count > 1;

            if (!selfRegistration && _serviceTypes.Count == 0)
            {
                return;
            }

            var serviceType = selfRegistration ? typeof(TImplementation) : _serviceTypes.Single();

            if (_value != null)
            {
                _serviceCollection.Add(new ServiceDescriptor(serviceType, _value));
            }
            else if (_factory != null)
            {
                _serviceCollection.Add(new ServiceDescriptor(serviceType, (provider) => _factory(new DependencyResolver(provider)), _serviceLifetime));
            }
            else
            {
                _serviceCollection.Add(new ServiceDescriptor(serviceType, typeof(TImplementation), _serviceLifetime));
            }

            if (selfRegistration)
            {
                foreach (var otherServiceType in _serviceTypes)
                {
                    _serviceCollection.Add(new ServiceDescriptor(otherServiceType, (provider) => provider.GetService<TImplementation>(), _serviceLifetime));
                }
            }
        }
    }
}
