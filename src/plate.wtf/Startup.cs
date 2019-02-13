using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using plate.wtf.Core;
using plate.wtf.Core.Interfaces;
using plate.wtf.Core.Plates;
using plate.wtf.Core.Plates.Interfaces;

namespace plate.wtf
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddTransient<IAtPlate, AtPlate>();
            services.AddTransient<IDePlate, DePlate>();
            services.AddTransient<IEsPlate, EsPlate>();
            services.AddTransient<IFrPlate, FrPlate>();
            services.AddTransient<IGbPlate, GbPlate>();
            services.AddTransient<IGgPlate, GgPlate>();
            services.AddTransient<IJpPlate, JpPlate>();
            services.AddTransient<INlPlate, NlPlate>();
            services.AddTransient<INoPlate, NoPlate>();
            services.AddTransient<IPlate, Plate>();
            services.AddTransient<IRuPlate, RuPlate>();
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
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
