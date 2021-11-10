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
            services.AddCors();
            services.AddControllers();
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "QuanLyBanDoGiaDung_API", Version = "v1" });
            //});
            //services.AddScoped<IPhotoService, PhotoService>();


            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            //services.AddAuthentication(x =>
            //{
            //  x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //  x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //.AddJwtBearer(x =>
            //{
            //  x.RequireHttpsMetadata = false;
            //  x.SaveToken = true;
            //  x.TokenValidationParameters = new TokenValidationParameters
            //  {
            //    ValidateIssuerSigningKey = true,
            //    IssuerSigningKey = new SymmetricSecurityKey(key),
            //    ValidateIssuer = false,
            //    ValidateAudience = false
            //  };
            //});

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                //// my API name as defined in Config.cs - new ApiResource... or in DB ApiResources table
                //o.Audience = Configuration["Settings:Authentication:ApiName"];
                //// URL of Auth server(API and Auth are separate projects/applications),
                //// so for local testing this is http://localhost:5000 if you followed ID4 tutorials
                //o.Authority = Configuration["Settings:Authentication:Authority"];

                //o.TokenValidationParameters = new TokenValidationParameters
                //{
                //    ValidateAudience = true,
                //    // Scopes supported by API as defined in Config.cs - new ApiResource... or in DB ApiScopes table
                //    ValidAudiences = new List<string>() {
                //    "api.read",
                //    "api.write"},
                //        ValidateIssuer = true
                //};
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
            services.AddTransient<IDiaChiBussiness, DiaChiBussiness>();
            services.AddTransient<IDiaChiRepository, DiaChiRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseSwagger();
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "QuanLyBanDoGiaDung_API v1"));
            }
            app.UseCors(x => x
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            app.UseAuthentication();
            app.UseHttpsRedirection();



            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
