using Bloggy.Models;
using Bloggy.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Bloggy
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            Envirnoment = env;
            var builder = new ConfigurationBuilder()
                .SetBasePath(Envirnoment.ContentRootPath)
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        public IHostingEnvironment Envirnoment { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            if (Envirnoment.IsDevelopment())
            {
                services.AddDbContext<BloggingDbContext>(options => options.UseInMemoryDatabase());
                services.AddTransient<IBlogService, InMemoryBlogService>();
            }

            services.Configure<Blog>(options => Configuration.GetSection("AppSettings:Blog").Bind(options));

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));

            if (Envirnoment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseMvc();
        }
    }
}
