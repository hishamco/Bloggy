using Bloggy.Models;
using Bloggy.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Bloggy
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            Envirnoment = env;
        }

        public IHostingEnvironment Envirnoment { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            if (Envirnoment.IsDevelopment())
            {
                services.AddDbContext<BloggingDbContext>(options => options.UseInMemoryDatabase());
                services.AddTransient<IBlogService, InMemoryBlogService>();
            }

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (Envirnoment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseMvc();
        }
    }
}
