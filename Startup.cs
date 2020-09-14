using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using WellStralerWebshop.Data;
using WellStralerWebshop.Data.Repositories;
using WellStralerWebshop.Filters;
using WellStralerWebshop.Models.Domain;

namespace WellStralerWebshop
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

            services.AddAuthentication("CookieAuth")
                .AddCookie("CookieAuth", config =>
                {
                    config.Cookie.Name = "InlogCookiess";
                    config.LoginPath = "/Account/Index";
                });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllersWithViews();

            services.AddLocalization(opts =>
            {
                opts.ResourcesPath = "Resources";
            });


            services.AddMvc()
                .AddViewLocalization(
                opts => { opts.ResourcesPath = "Resources"; })
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();

            services.Configure<RequestLocalizationOptions>(opts =>
            {
                var supportedCultures = new List<CultureInfo>
                {
                    new CultureInfo("en"),
                    new CultureInfo("nl"),
                    new CultureInfo("fr"),
                };

                opts.DefaultRequestCulture = new RequestCulture("nl");
                opts.SupportedCultures = supportedCultures;
                opts.SupportedUICultures = supportedCultures;

            });

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IKlantRepository, KlantRepository>();
            services.AddScoped<IKlantLoginRepository, KlantLoginRepository>();
            services.AddScoped<IOnlineBestelLijnRepository, OnlineBestelLijnenRepository>();
            services.AddScoped<IOnlineBestellingRepository, OnlineBestellingRepository>();
            services.AddScoped<ITransportRepository,TransportRepository>();
            services.AddScoped<IFactuurLijnRepository, FactuurLijnRepository>();
            services.AddScoped<IFactuurRepository, FactuurRepository>();
            services.AddScoped<KlantFilter>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Product}/{action=Index}/{id?}");
                
            });
        }
    }
}
