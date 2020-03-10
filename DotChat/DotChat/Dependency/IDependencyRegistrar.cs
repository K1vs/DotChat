namespace K1vs.DotChat.Dependency
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IDependencyRegistrar
    {
        IDependencyRegistrationBuilder<TImplementation> Register<TImplementation>();
        IDependencyRegistrationBuilder<TImplementation> Register<TImplementation>(TImplementation implementation) where TImplementation : class;
        IDependencyRegistrationBuilder<TImplementation> Register<TImplementation>(Func<IDependencyResolver, TImplementation> factory);
    }
}
