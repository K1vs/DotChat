namespace K1vs.DotChat.Dependency
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IDependencyRegistrationBuilder<out TImplementation>
    {
        IDependencyRegistrationBuilder<TImplementation> As<TService>();
        IDependencyRegistrationBuilder<TImplementation> As(Type type);
        IDependencyRegistrationBuilder<TImplementation> AsSelf();

        IDependencyRegistrationBuilder<TImplementation> AsSingleton();
        IDependencyRegistrationBuilder<TImplementation> AsTransient();
        IDependencyRegistrationBuilder<TImplementation> AsScoped();

        void Build();
    }
}
