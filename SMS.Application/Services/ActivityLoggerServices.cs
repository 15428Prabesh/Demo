using SMS.Core.Entities;
using SMS.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Application.Services
{
	public class ActivityLoggerServices 
	{
		private readonly IActivityLogRepository _logRepository;

		public ActivityLoggerServices(IActivityLogRepository logRepository)
		{
			_logRepository = logRepository;
		}

		public async Task LogUserAction(int userId, string action, string details)
		{
			try
			{
				var log = new UserActivityLog
				{
					UserId = userId,
					Action = action,
					Timestamp = DateTime.UtcNow,
					Details = details
				};
				await _logRepository.AddLogAsync(log);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				throw;
			}
			
		}
	}
}
