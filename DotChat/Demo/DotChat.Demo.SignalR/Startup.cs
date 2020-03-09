using K1vs.DotChat.Demo.SignalR;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace K1vs.DotChat.Demo.SignalR
{
    using System;
    using System.IO;
    using Microsoft.Owin.FileSystems;
    using Microsoft.Owin.StaticFiles;
    using Owin;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseStaticFiles("/wwwroot");
            app.MapSignalR();
        }
    }
}
