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
        protected readonly IComponentContext ComponentContext;

        public DependencyResolver(IComponentContext componentContext)
        {
            ComponentContext = componentContext;
        }

        public virtual TService Resolve<TService>()
        {
            return ComponentContext.Resolve<TService>();
        }

        public virtual object Resolve(Type type)
        {
            return ComponentContext.Resolve(type);
        }
    }
}
