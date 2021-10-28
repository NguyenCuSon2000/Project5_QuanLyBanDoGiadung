using BLL;
using DAL;
using DAL.Helper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using QuanLyBanDoGiaDung_API.Interfaces;
using QuanLyBanDoGiaDung_API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyBanDoGiaDung_API
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "QuanLyBanDoGiaDung_API", Version = "v1" });
            });
            services.AddScoped<IPhotoService, PhotoService>();

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
            services.AddTransient<IUserBussiness, UserBussiness>();
            services.AddTransient<IUserRepository, UserRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "QuanLyBanDoGiaDung_API v1"));
            }
            app.UseCors(x => x
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
