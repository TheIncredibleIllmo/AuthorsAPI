using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthorsAPI.Contexts;
using AuthorsAPI.Data;
using AuthorsAPI.Entities;
using AuthorsAPI.Helpers.Filters;
using AuthorsAPI.Models.DTO;
using AuthorsAPI.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ILogger = AuthorsAPI.Services.ILogger;

namespace AuthorsAPI
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
            //Each time the service is required an instance of the class is served.
            //services.AddTransient<IBClass, BClass>();

            //One peer Http request, in the case of a Http request the same instance is served
            //services.AddScoped<IBClass,BClass>();

            //The same instance is given
            //services.AddSingleton<IBClass,BClass>();

            //Enables Caching
            //services.AddResponseCaching();

            services.AddScoped<ValuesRepository>();

            services.AddScoped<CustomActionFilter>();
            services.AddTransient<Microsoft.Extensions.Hosting.IHostedService, WriteToFileHostedService>();

            //Auth
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer();

            services.AddTransient<ILogger, Logger>();

            //Connects to the Db
            services.AddDbContext<ApplicationDbContext>(o =>
                o.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString")));

            //Avoids reference looping
            services.AddControllers().AddNewtonsoftJson(o =>
                 o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            //AutoMapper
            services.AddAutoMapper(config =>
            {
                config.CreateMap<Author, AuthorDTO>();
                config.CreateMap<AuthorCreateDTO, Author>().ReverseMap();

                config.CreateMap<Book, BookDTO>();
            },
            typeof(Startup));

            services.AddMvc(options =>
            {
                options.Filters.Add(new CustomExceptionFilter());

                //If dependency injection
                //options.Filters.Add(typeof(CustomExceptionFilter));
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            //app.UseResponseCaching();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
