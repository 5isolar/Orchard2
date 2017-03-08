using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Modules;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Orchard.Environment.Shell;
using Orchard.StorageProviders.FileSystem;

namespace Orchard.Media
{
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IMediaFileStore>(serviceProvider =>
            {
                var shellOptions = serviceProvider.GetRequiredService<IOptions<ShellOptions>>();
                var shellSettings = serviceProvider.GetRequiredService<ShellSettings>();

                (string requestPath, string mediaPath) = GetSettings(shellOptions.Value, shellSettings);

                return new MediaFileStore(new FileSystemStore(requestPath, mediaPath));
            });
        }

        public override void Configure(IApplicationBuilder app, IRouteBuilder routes, IServiceProvider serviceProvider)
        {
            var shellOptions = serviceProvider.GetRequiredService<IOptions<ShellOptions>>();
            var shellSettings = serviceProvider.GetRequiredService<ShellSettings>();

            (string requestPath, string mediaPath) = GetSettings(shellOptions.Value, shellSettings);

            var env = serviceProvider.GetRequiredService<IHostingEnvironment>();
            var mediaPhysicalPath = env.ContentRootFileProvider.GetFileInfo(mediaPath).PhysicalPath;

            if (!Directory.Exists(mediaPhysicalPath))
            {
                Directory.CreateDirectory(mediaPhysicalPath);
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                RequestPath = requestPath,
                FileProvider = new PhysicalFileProvider(mediaPhysicalPath)
            });
        }

        private (string requestPath, string mediaPath) GetSettings(ShellOptions shellOptions, ShellSettings shellSettings)
        {
            return (
                requestPath: (string.IsNullOrEmpty(shellSettings.RequestUrlPrefix) ? "" : "/" + shellSettings.RequestUrlPrefix) + "/media", 
                mediaPath:  Path.Combine(shellOptions.ShellsRootContainerName, shellOptions.ShellsContainerName, shellSettings.Name, "Media")
                );
        }
    }
}
