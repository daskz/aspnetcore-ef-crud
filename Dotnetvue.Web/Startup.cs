using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dotnetvue.Data;
using Dotnetvue.Data.Models;
using Dotnetvue.Web.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Dotnetvue.Web
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
            services.AddDbContext<ApplicationDbContext>(x => x.UseInMemoryDatabase("InMemoryDb"));
            services.AddControllers();

            var optionsSection = Configuration.GetSection(nameof(AppOptions));
            services.Configure<AppOptions>(optionsSection);

            ConfigureJwtAuth(services, optionsSection.Get<AppOptions>());

            services.AddHttpContextAccessor();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IFinanceService, FinanceService>();
            services.AddScoped<IRequestNumberProvider, RequestNumberProvider>();
        }

        private static void ConfigureJwtAuth(IServiceCollection services, AppOptions options)
        {
            services.AddAuthentication(authOptions =>
                {
                    authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(jwtBearerOptions =>
                {
                    jwtBearerOptions.RequireHttpsMetadata = false;
                    jwtBearerOptions.SaveToken = true;
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(options.JwtSecretKey)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IUserService userService)
        {
            SeedUsersToDatabase(userService);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void SeedUsersToDatabase(IUserService userService)
        {
            userService.Create(new User {Id = Guid.Parse("6F85E76A-6324-433A-AF4E-1FE4D9480B2C"), Username = "johndoe"}, "johndoe");
            userService.Create(new User { Id = Guid.Parse("A6F61045-9E1E-46BF-A56E-16E123B325B8"), Username = "johnsmith" }, "johnsmith");
        }
    }
}
