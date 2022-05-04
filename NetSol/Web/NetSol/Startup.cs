using Core.Constant;
using Core.Extension;
using Core.JWT.JWTModel;
using Core.JWT.LoadSetting;
using Core.Models;
using Core.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NetSol.Utils;
using Repository.Contract.Infrastructure;
using Repository.Contract.Models;
using Repository.Infrastructure;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetSol
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
            services.AddSystemSetting(Configuration.GetSection("SystemHelper").Get<SystemHelperModel>());
            services.AddJWTSetting(Configuration.GetSection("JWT").Get<JWT>());

            services.AddDbContext<IDbContext,AppDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddServiceFromAttribute();
            // Register Repository services
            // services.AddRepository();


            // Set up JWT Auth
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = SystemHelper.JWTSetting.Issuer,
                    ValidateAudience = true,
                    ValidAudience = SystemHelper.JWTSetting.Audience,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,

                    IssuerSigningKey = new SymmetricSecurityKey(StringUltis.ToByteArray(SystemHelper.JWTSetting.SecrectKey))
                };
            });
            services.AddHttpContextAccessor();

            services.Configure<RouteOptions>(options => {
                options.AppendTrailingSlash = false;        // Thêm dấu / vào cuối URL
                options.LowercaseUrls = true;               // url chữ thường
                options.LowercaseQueryStrings = false;      // không bắt query trong url phải in thường
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "NetSol", Version = SystemHelper.Setting.Version });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseSwagger();
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", $"NetSol {SystemHelper.Setting.Version}"));
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", $"NetSol {SystemHelper.Setting.Version}"));

            app.UseSystemSetting();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
