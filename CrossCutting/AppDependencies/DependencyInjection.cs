using Domain.Abstractions;
using MediatR;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Application.UserCases.Users.Commands.Login;
using Application.Mappers;

namespace CrossCutting.AppDependencies
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("TestDataBase"));
            services.AddScoped<Microsoft.AspNet.Identity.IPasswordHasher, Microsoft.AspNet.Identity.PasswordHasher>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(typeof(MappingProfile));
            var myhandlers = AppDomain.CurrentDomain.Load("Application");
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(myhandlers));
            
            return services;
        }
    }
}
