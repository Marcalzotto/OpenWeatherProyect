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
            services.AddControllers();

            services.AddSwaggerGen(options =>
            {
                var groupname = "v1";

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

            //configure services
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IBranchOfficeService, BranchOfficeService>();
            services.AddScoped<IWeatherConditionService, WeatherConditionService>();

            //configure repositories
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IBranchOfficeRepository, BranchOfficeRepository>();
            services.AddScoped<IWeatherConditionRepository, WeatherConditionRepository>();


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

            app.UseMvc();

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "OpenWeatherAPI V1"); });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
