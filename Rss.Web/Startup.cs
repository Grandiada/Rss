using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rss.Core;
using Rss.Core.Models.Context;
using Rss.Web.Controllers;

namespace Rss.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            #region Ioc
            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterType<RssRecordService>().SingleInstance();

            builder.RegisterInstance<Func<RssDbContext>>(
                () => new RssDbContext(Configuration.GetConnectionString("DefaultConnection")));

            var container = builder.Build();

            return new AutofacServiceProvider(container);
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: null,
                    template: "",
                    defaults: new
                    {
                        controller = "Home",
                        action = nameof(HomeController.Index)
                    });

                routes.MapRoute(
                    name: null,
                    template: "index-ajax/",
                    defaults: new
                    {
                        controller = "Home",
                        action = nameof(HomeController.IndexAjax)
                    });

                routes.MapRoute(
                    name: null,
                    template: "get-data/",
                    defaults: new
                    {
                        controller = "Home",
                        action = nameof(HomeController.LoadData)
                    });
            });
        }
    }
}
