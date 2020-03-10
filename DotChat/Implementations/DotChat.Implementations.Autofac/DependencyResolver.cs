namespace K1vs.DotChat.Implementations.Autofac
{
    using global::Autofac;
    using K1vs.DotChat.Dependency;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class DependencyResolver : IDependencyResolver
    {
        private readonly IComponentContext _componentContext;

        public DependencyResolver(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }

        public TService Resolve<TService>()
        {
            return _componentContext.Resolve<TService>();
        }

        public object Resolve(Type type)
        {
            return _componentContext.Resolve(type);
        }
    }
}
