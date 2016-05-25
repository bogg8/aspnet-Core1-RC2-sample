﻿namespace Microsoft.AspNetCore.Builder
{
    using System.IO;

    using Microsoft.AspNetCore.Hosting.Internal;
    using Microsoft.Extensions.FileProviders;
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseNodeModules(this IApplicationBuilder app, HostingEnvironment env)
        {
            var path = Path.Combine(env.ContentRootPath, "node_modules");
            var provider = new PhysicalFileProvider(path);

            var options = new StaticFileOptions();
            options.RequestPath = "/node_modules";
            options.FileProvider = provider;

            app.UseStaticFiles(options);
            return app;
        }
    }
}
