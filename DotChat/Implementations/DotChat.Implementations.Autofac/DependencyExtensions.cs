namespace K1vs.DotChat.Implementations.Autofac
{
    using global::Autofac;
    using K1vs.DotChat.Dependency;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class DependencyExtensions
    {
        public static ContainerBuilder RegisterDotChat(this ContainerBuilder serviceCollection, IDependencyModule module, bool rootModule = true)
        {
            module.Register(new DependencyRegistrar(serviceCollection), rootModule);
            return serviceCollection;
        }
    }
}
