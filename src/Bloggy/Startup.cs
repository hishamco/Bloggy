using Bloggy.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

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

        public BloggingContext Db { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            if (Environment.IsDevelopment())
            {
                services.AddDbContext<BloggingContext>(options => options.UseInMemoryDatabase());
            }
            else
            {
                services.AddDbContext<BloggingContext>(options => options.UseSqlite(Configuration["Data:DefaultConnection:ConnectionString"]));
            }

            services.Configure<AppSettings>(options => Configuration.GetSection("AppSettings").Bind(options));

            services.AddAuthentication();

            services.AddMvc();

            services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory, BloggingContext db)
        {
            if (Environment.IsDevelopment())
            {
                Db = db;
                loggerFactory.AddConsole(Configuration.GetSection("Logging"));
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                AddSeedData();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true
            });

            app.UseStaticFiles();

            app.UseMvc();
        }

        private void AddSeedData()
        {
            for (int i = 1; i <= 10; i++)
            {
                var post = new Post
                {
                    Id = i,
                    Title = "What is Lorem Ipsum?",
                    Slug = "what-is-lorem-ipsum-" + i,
                    Excerpt = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s",
                    Content = @"**Lorem Ipsum** is simply dummy text of the printing and typesetting industry. **Lorem Ipsum** has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing **Lorem Ipsum** passages, and more recently with desktop publishing software like Aldus PageMaker including versions of **Lorem Ipsum**.",
                    IsPublished = true,
                    PublishedAt = new DateTime(2016, 12, 2),
                    Tags = "Lorem Ipsum,lorem,ipsum"
                };
                post.Comments = new List<Comment>();

                for (int j = 1; j < 3; j++)
                {
                    var comment = new Comment()
                    {
                        Author = "Lorem Ipsum",
                        Email = "lorem@ipsum.com",
                        Content = "Lorem Ipsum is simply dummy text of the printing and typesetting industry.",
                        Website = "http://www.lipsum.com",
                        PublishedAt = new DateTime(2016, 12, 14),
                        PostId = i,
                        Post = post
                    };
                    post.Comments.Add(comment);
                }
                Db.Posts.Add(post);
            }

            Db.SaveChanges();
        }
    }
}
