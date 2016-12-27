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
            Environment = env;
            var builder = new ConfigurationBuilder()
                .SetBasePath(Environment.ContentRootPath)
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        public IHostingEnvironment Environment { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            if (Environment.IsDevelopment())
            {
                services.AddDbContext<BloggingContext>(options => options.UseInMemoryDatabase());
                services.AddSingleton<IBlogService,InMemoryBlogService>();
            }
            else
            {
                services.AddDbContext<BloggingContext>(options => options.UseSqlite("Filename=Blogging.db"));
                services.AddSingleton<IBlogService, BlogService>();
            }

            services.Configure<Blog>(options => Configuration.GetSection("AppSettings:Blog").Bind(options));

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));

            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseMvc();
        }
    }
}
