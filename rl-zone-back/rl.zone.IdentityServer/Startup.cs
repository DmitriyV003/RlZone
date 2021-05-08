using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using rl.zone.IdentityServer.Models;

namespace rl.zone.IdentityServer
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
            services.AddIdentity<AppUser, Role>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<JwtSettings>(Configuration.GetSection("Jwt"));

            services.AddIdentity<AppUser, Role>(opt =>
            {
                opt.Password.RequiredLength = 8;
                opt.Lockout.MaxFailedAccessAttempts = 5;
                opt.Password.RequireNonAlphanumeric = false;
            });
            
            var connectionString = Configuration["ConnectionStrings:ApplicationDbConnectionString"];
            services.AddDbContext<ApplicationDbContext>(c => c.UseSqlServer(connectionString));

            services.AddAuthorization(c =>
            {
                c.AddPolicy("ItemRunner", p =>
                {
                    p.RequireRole();
                });
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}