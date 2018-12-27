using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MvcMovie.Models;

namespace MvcMovie
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IWebHost hosts = CreateWebHostBuilder(args).Build();


            /*
             * 由于 MvcMovieContext 是一个 scope 服务，rootSeriveProvider并不提供，构建scope后从中获取 MvcMovieContext
             * /
            IServiceProvider serviceProvider = hosts.Services;

            try
            {
                MvcMovieContext context = serviceProvider.GetRequiredService<MvcMovieContext>();
                context.Database.Migrate();
                SeedData.Initialize(serviceProvider);
            }
            catch (Exception ex)
            {
                var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred seeding the DB");
            }
            */
            using (var scope = hosts.Services.CreateScope())
            {
                IServiceProvider serviceProvider = scope.ServiceProvider;

                try
                {
                    MvcMovieContext context = serviceProvider.GetRequiredService<MvcMovieContext>();
                    context.Database.Migrate(); 
                    SeedData.Initialize(serviceProvider);
                }
                catch(Exception ex)
                {
                    var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex,"An error occurred seeding the DB");
                }
            }

            hosts.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
