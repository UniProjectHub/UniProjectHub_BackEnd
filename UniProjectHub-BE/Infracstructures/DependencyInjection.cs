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
using Infracstructures.Services;
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
        {    
            services.AddScoped<IUnitOfWork, UnitOfWork>();



            // Register Project-related services
            services.AddTransient<IProjectRepository, ProjectRepository>();
            services.AddTransient<IProjectService, ProjectService>();

            // Register Task-related services
            services.AddTransient<ITaskRepository, TaskRepository>();
            services.AddTransient<ITaskService, TaskSevice>();

            // Register SubTask-related services
            services.AddTransient<ISubTaskRepository, SubTaskRepository>();
            services.AddTransient<ISubTaskService, SubTaskService>();

            // Register MemberInTask-related services
            services.AddTransient<IMemberInTaskService, MemberInTaskService>();
            services.AddTransient<IMemberInTaskRepository, MemberInTaskRepository>();

            // Register Blog-related services
            services.AddTransient<IBlogRepository, BlogRepository>();
            services.AddTransient<IBlogService, BlogService>();

            // Register Category-related services
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<ICategoryService, CategoryService>();

            // Register Member-related services
            services.AddTransient<IMemberRepository, MemberRepository>();
            services.AddTransient<IMemberService, MemberService>();

            // Use local DB
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
            services.AddTransient<IPaymentRepository, PaymentRepository>();
            services.AddTransient<IPaymentService, PaymentService>();
            services.AddTransient<IFileValidator, FileViewModelValidator>();
            services.AddMemoryCache();

            // Register AutoMapper
            services.AddAutoMapper(typeof(MapperConfigs).Assembly);
            services.AddControllers();




            return services;
        }

    }
}
