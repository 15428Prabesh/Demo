using Microsoft.EntityFrameworkCore;
using SMS.Core.Entities;
using SMS.Core.Interfaces;
using SMS.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Infrastructure.Repositories
{
	public class ActivityLogRepository : IActivityLogRepository
	{
		private readonly ApplicationDbContext _context;

		public ActivityLogRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task AddLogAsync(UserActivityLog log)
		{
			await _context.Set<UserActivityLog>().AddAsync(log);
			await _context.SaveChangesAsync();
		}

		//public async Task<IEnumerable<UserActivityLog>> GetUserLogsAsync(string userId)
		//{
		//	return await _context.Set<UserActivityLog>().Where(log => log.UserId == userId).ToListAsync();
		//}
	}
}
