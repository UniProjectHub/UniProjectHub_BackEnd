using Application.InterfaceRepositories;
using Application.InterfaceServies;
using Application.IValidators;
using Application.Services;
using Application.Validators;
using Domain.Interfaces;
using Domain.Models;
using Infracstructures.Mappers;
using Infracstructures.Repositories;
using Infracstructures.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Infracstructures
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfractstructure(this IServiceCollection services, IConfiguration config)
        {    // Use local DB
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(config.GetConnectionString("UniProject")));

            services.AddCors(options =>
            {
                options.AddPolicy(name: "_myAllowSpecificOrigins",
                                  policy =>
                                  {
                                      policy.WithOrigins(
                                          "https://educationmanagementapptest.web.app",
                                          "https://educationmanagementfaapp.web.app",
                                          "https://fsoftinternmockproject.web.app")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod();
                                  });
            });
            services.AddControllers().AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });
            services.AddControllers().AddNewtonsoftJson(o =>
            {
                o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddScoped<ITokenService, TokenService>();
            services.AddTransient<IFileManageRepository, FileManageRepository>();
            services.AddTransient<IFileManageService, FileManageService>();
            services.AddTransient<IFileValidator, FileViewModelValidator>();

            // Register AutoMapper
            services.AddAutoMapper(typeof(MapperConfigs).Assembly);

            


            return services;
        }

    }
}
