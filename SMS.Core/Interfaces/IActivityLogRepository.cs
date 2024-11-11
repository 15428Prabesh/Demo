using SMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Core.Interfaces
{
	public interface IActivityLogRepository
	{
		Task AddLogAsync(UserActivityLog log);
		//Task<IEnumerable<UserActivityLog>> GetUserLogsAsync(string userId);
	}
}
