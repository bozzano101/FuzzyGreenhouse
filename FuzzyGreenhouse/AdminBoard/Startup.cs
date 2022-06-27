using AdminBoard.Data;
using AdminBoard.Infrastructure.IdentityUserClaims;
using AdminBoard.Infrastructure.Services;
using AdminBoard.Models.Identity;
using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
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

        public const string Enviroment = "TestServer";

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddNotyf(config =>
            {
                config.DurationInSeconds = 3;
                config.IsDismissable = true;
                config.Position = NotyfPosition.BottomRight                                                                                                                                                                     ;
            });
                             
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            services.AddRazorPages(options =>
            {
                options.Conventions.AddAreaPageRoute("Identity", "/Account/Register", "/register");
                options.Conventions.AddAreaPageRoute("Identity", "/Account/Login", "/login");
                options.Conventions.AddAreaPageRoute("Identity", "/Account/Logout", "/logout");
                options.Conventions.AddAreaPageRoute("Identity", "/Account/ForgotPassword", "/forgot-password");
            });

            var connectionString = Configuration.GetConnectionString(Enviroment);
            var serverVersion = ServerVersion.AutoDetect(connectionString);

            
            services.AddDbContext<FuzzyGreenhouseDbContext>(option => option.UseMySql(connectionString, serverVersion, b => b.SchemaBehavior(MySqlSchemaBehavior.Ignore)));

            services.AddDbContext<IdentityContext>(option => option.UseMySql(connectionString, serverVersion, b => b.SchemaBehavior(MySqlSchemaBehavior.Ignore)));
            services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false).AddRoles<IdentityRole>().AddDefaultUI().AddEntityFrameworkStores<IdentityContext>();

            services.AddScoped<IUserClaimsPrincipalFactory<User>, AppUserClaimsPrincipalFactory>();
            services.AddScoped<VariableService, VariableService>();
            services.AddScoped<ValuesService, ValuesService>();
            services.AddScoped<RuleService, RuleService>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;

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

            var identity = serviceScope.ServiceProvider.GetService<IdentityContext>();
            if (identity != null && identity.Database != null)
            {
                identity.Database.Migrate();
            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            UpgradeDatabase(app);
            app.UseNotyf();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
                app.UseHsts();
            }
            app.UseStatusCodePagesWithReExecute("/status-code", "?code={0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();

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
