using AdminBoard.Data;
using AdminBoard.Infrastructure;
using AdminBoard.Infrastructure.IdentityUserClaims;
using AdminBoard.Models.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;

namespace AdminBoard
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.Strict;
            });

            services.AddDbContext<FuzzyGreenhouseDbContext>(option => option.UseMySql(Configuration.GetConnectionString("LocalServer"), ServerVersion.AutoDetect(Configuration.GetConnectionString("LocalServer")), b => b.SchemaBehavior(MySqlSchemaBehavior.Ignore)));

            services.AddDbContext<IdentityContext>(option => option.UseMySql(Configuration.GetConnectionString("LocalServer"), ServerVersion.AutoDetect(Configuration.GetConnectionString("LocalServer")), b => b.SchemaBehavior(MySqlSchemaBehavior.Ignore)));
            services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false).AddRoles<IdentityRole>().AddEntityFrameworkStores<IdentityContext>();

            services.AddScoped<IUserClaimsPrincipalFactory<User>, AppUserClaimsPrincipalFactory>();

            services.Configure<IdentityOptions>(options =>
            {
                // Default Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // Default Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Default SignIn settings.
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;

                // Default User settings.
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "AdminBoard";
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                // You might want to only set the application cookies over a secure connection:
                // options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.SlidingExpiration = true;
            });

            services.PostConfigure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme, options =>
            {
                options.AccessDeniedPath = "/access-denied";
                options.LoginPath = "/login";
                options.LogoutPath = "/logout";

                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            });

            services.AddAntiforgery();

            services.AddControllersWithViews(options =>
            {
                // Slugify routes so that we can use /employee/employee-details/1 instead of
                // the default /Employee/EmployeeDetails/1
                options.Conventions.Add(
                    new RouteTokenTransformerConvention(
                        new SlugifyParameterTransformer()));

                // Enable Antiforgery feature by default on all controller actions
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });

            services.AddRazorPages(options =>
            {
                options.Conventions.AddAreaPageRoute("Identity", "/Account/Register", "/register");
                options.Conventions.AddAreaPageRoute("Identity", "/Account/Login", "/login");
                options.Conventions.AddAreaPageRoute("Identity", "/Account/Logout", "/logout");
                options.Conventions.AddAreaPageRoute("Identity", "/Account/ForgotPassword", "/forgot-password");
            }); // MOZDA TREBA DA SE DODA COMPATIBILITYVERSION

            services.AddAuthorization(options => { options.DefaultPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build(); });

            services.AddMemoryCache();

        }

        private void UpgradeDatabase(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var fuzzyGreenhouse = serviceScope.ServiceProvider.GetService<FuzzyGreenhouseDbContext>();
            if (fuzzyGreenhouse != null && fuzzyGreenhouse.Database != null)
            {
                fuzzyGreenhouse.Database.Migrate();
            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            UpgradeDatabase(app);
            app.UseForwardedHeaders();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseStatusCodePagesWithReExecute("/status-code", "?code={0}");

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute("default", "{controller=home}/{action=index}/{id?}");
            });
        }
    }
}
