using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Core.Entities
{
	public class UserActivityLog
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		//public string Email { get; set; } // or UserName depending on your design
		public string Action { get; set; }
		public DateTime Timestamp { get; set; }
		public string Details { get; set; }
	}
}
