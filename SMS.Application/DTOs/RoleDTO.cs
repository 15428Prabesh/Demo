using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Application.DTOs
{
	public class RoleDTO
	{
		public string Name { get; set; }
		public bool isActive { get; set; } = true;
		public string CreatedBy { get; set; }
		public string? UpdatedBy { get; set; }
		public bool isModified { get; set; } = false;
    }
}
