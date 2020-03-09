using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;

[assembly: OwinStartup(typeof(DotChat.Demo.SignalR.Startup))]

namespace DotChat.Demo.SignalR
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            string root = AppDomain.CurrentDomain.BaseDirectory;
            var physicalFileSystem = new PhysicalFileSystem(Path.Combine(root, "Client"));

            var fileServerOptions = new FileServerOptions
            {
                EnableDefaultFiles = true,
                FileSystem = physicalFileSystem,
                EnableDirectoryBrowsing = true
            };

            fileServerOptions.StaticFileOptions.ServeUnknownFileTypes = true;
            app.UseFileServer(fileServerOptions);


        }
    }
}
