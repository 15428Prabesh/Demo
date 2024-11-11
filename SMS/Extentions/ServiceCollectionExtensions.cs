using SMS.Application.Services;
using SMS.Core.Interfaces;
using SMS.Infrastructure.Repositories;
using SMS.Infrastructure.Sevices;
using System.Runtime.CompilerServices;

namespace SMS.Extentions
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
		{
		
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
			services.AddScoped<IActivityLogRepository, ActivityLogRepository>();
			services.AddScoped<UserServices>();
			services.AddScoped(typeof(CrudServices<>));
			services.AddScoped<ActivityLoggerServices>();

			services.AddSingleton<IJwtService>(provider => new JwtService(
				configuration["JwtSettings:Key"],
				configuration["JwtSettings:Issuer"],
				configuration["JwtSettings:Audience"]));

			return services;
		}
	}
}
