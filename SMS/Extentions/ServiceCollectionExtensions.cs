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
			services.AddScoped<RoleServices>();
			services.AddScoped(typeof(CrudServices<>));
			services.AddScoped<ActivityLoggerServices>();

			services.AddSingleton<IJwtService>(provider => new JwtService(
				configuration["JwtSettings:Key"],
				configuration["JwtSettings:Issuer"],
				configuration["JwtSettings:Audience"]));

			services.AddSingleton<IEmailService>(provider => new EmailServices(
				configuration["EmailSettings:SmtpServer"],
				int.Parse(configuration["EmailSettings:Port"]),
				configuration["EmailSettings:Username"],
				configuration["EmailSettings:Password"]
			));

			return services;
		}
	}
}
