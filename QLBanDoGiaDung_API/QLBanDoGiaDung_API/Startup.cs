using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using DAL;
using DAL.Helper;
using Helper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using QLBanDoGiaDung_API.Interfaces;
using QLBanDoGiaDung_API.Services;
using QLBanDoGiaDung_API.Settings;

namespace QLBanDoGiaDung_API
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
               .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddCors();
            services.AddControllers();
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            services.AddTransient<IMailService, MailService>();
   
            services.AddSwaggerGen(c =>
                    {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "QuanLyBanDoGiaDung_API", Version = "v1" });
              });

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddTransient<IDatabaseHelper, DatabaseHelper>();

            services.AddTransient<INhomSanPhamBussiness, NhomSanPhamBussiness>();
            services.AddTransient<INhomSanPhamRepository, NhomSanPhamRepository>();
            services.AddTransient<ILoaiSanPhamBussiness, LoaiSanPhamBussiness>();
            services.AddTransient<ILoaiSanPhamRepository, LoaiSanPhamRepository>();
            services.AddTransient<ISanPhamBussiness, SanPhamBussiness>();
            services.AddTransient<ISanPhamRepository, SanPhamRepository>();
            services.AddTransient<IHangSanPhamBussiness, HangSanPhamBussiness>();
            services.AddTransient<IHangSanPhamRepository, HangSanPhamRepository>();
            services.AddTransient<IDonHangBussiness, DonHangBussiness>();
            services.AddTransient<IDonHangRepository, DonHangRepository>();
            services.AddTransient<INguoiDungBussiness, NguoiDungBussiness>();
            services.AddTransient<INguoiDungRepository, NguoiDungRepository>();
            services.AddTransient<ITinTucBussiness, TinTucBussiness>();
            services.AddTransient<ITinTucRepository, TinTucRepository>();
            services.AddTransient<IDiaChiBussiness, DiaChiBussiness>();
            services.AddTransient<IDiaChiRepository, DiaChiRepository>();
            services.AddTransient<IThongKeBussiness, ThongKeBussiness>();
            services.AddTransient<IThongKeRepository, ThongKeRepository>();
          }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
           
            if (env.IsDevelopment())
            {
              app.UseDeveloperExceptionPage();
              app.UseSwagger();
              app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BlogManagement v1"));
            }
            //app.UseSwaggerUI(c =>
            //{
            //  c.SwaggerEndpoint("./v1/swagger.json", "My API V1"); //originally "./swagger/v1/swagger.json"
            //});
            app.UseCors(x => x
                              .AllowAnyOrigin()
                              .AllowAnyMethod()
                              .AllowAnyHeader());

            app.UseRouting();
            app.UseDeveloperExceptionPage();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
