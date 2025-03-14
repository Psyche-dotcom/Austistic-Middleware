﻿using AlpaStock.Api.MapingProfile;
using AlpaStock.Core.Repositories.Interface;
using AlpaStock.Infrastructure.Service.Implementation;
using AlpaStock.Infrastructure.Service.Interface;
using Austistic.Core.Repositories.Implementation;
using Austistic.Core.Repositories.Interface;
using Austistic.Infrastructure.Service.Implementation;

namespace AlpaStock.Api.Extension
{
    public static class RegisteredServices
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<IAccountRepo, AccountRepo>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped(typeof(IAutisticRepository<>), typeof(AutisticRepository<>));
            services.AddScoped<IGenerateJwt, GenerateJwt>();
            services.AddScoped<IEmailServices, EmailService>();
            services.AddScoped<ICloudinaryService, CloudinaryService>();
            services.AddAutoMapper(typeof(ProjectProfile));
            services.AddHttpClient();
        }
    }
}
