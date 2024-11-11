using SMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Core.Interfaces
{
	public interface IJwtService
	{
		string GenerateJwtToken(User user);
	}
}
