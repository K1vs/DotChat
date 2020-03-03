namespace K1vs.DotChat.Dependency
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IDependencyModule
    {
        void Register(IDependencyRegistrar registrar, bool rootModule);
    }
}
