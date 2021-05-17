using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using OpenWeatherAPI.Business.Infrastructure;
using OpenWeatherAPI.Business.Services;
using OpenWeatherAPI.BusinessContracts.Services;
using OpenWeatherAPI.Data.DataEntities;
using OpenWeatherAPI.Data.Repositories;
using OpenWeatherAPI.DataContracts.Repositories;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;

namespace OpenWeatherAPI
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
            //valores de configuracion que utilizares entre los del jwt
            var key = Configuration.GetSection("JwtConfig").GetSection("SecrectScurityKey").Value;
            var jwtParamConf = Configuration.GetSection("JwtConfig").GetSection("IssuerAudience").Value;

            //registramos el JWT authenticaction middleware en el IServiceCollection
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) //establecemos el esquema de autenticacion
            .AddJwtBearer(options =>
            {
                //establecemos parametros a usar para validar JWT.
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtParamConf,
                    ValidAudience = jwtParamConf,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                    ClockSkew = TimeSpan.Zero //evita que se hagan ajustes temporales en el token que ya expiro 
                };
            });

           
            services.AddControllers();

            services.AddSwaggerGen(options =>
            {
                var groupname = "v3";

                options.SwaggerDoc(groupname, new OpenApiInfo
                {
                    Title = $"OpenWeatherApi {groupname}",
                    Version = groupname,
                    Description="OpenWeatherAPI",
                });
                
            });

            services.AddMvc().AddMvcOptions(opt => opt.EnableEndpointRouting = false);
            services.AddMvc().AddNewtonsoftJson(
               options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.AddControllers().AddNewtonsoftJson();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .WithOrigins(Configuration.GetConnectionString("OpenWeatherWebApp"))
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            services.AddDbContext<OpenWeatherDBContext>(options =>
            {
                if (!options.IsConfigured) 
                {
                    options.UseSqlServer(Configuration.GetConnectionString("Default"));
                }
            });


            //configure services //registramos dependencia - implementacion en DI container
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IBranchOfficeService, BranchOfficeService>();
            services.AddScoped<IWeatherConditionService, WeatherConditionService>();
            services.AddScoped<IAuthService, AuthService>();

            //configure repositories
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IBranchOfficeRepository, BranchOfficeRepository>();
            services.AddScoped<IWeatherConditionRepository, WeatherConditionRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();


            //configure automapper 
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();

            services.AddSingleton(mapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMvc();

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v3/swagger.json", "OpenWeatherAPI V1"); });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
