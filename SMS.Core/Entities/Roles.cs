using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Core.Entities
{
	public class Roles
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public bool isActive { get; set; } = true;
        public string CreatedBy { get; set; }
        public string? UpdatedBy { get; set;}
        public bool isModified { get; set; } = false;
        public DateTime CreatedAt { get; set; }
    }
}
