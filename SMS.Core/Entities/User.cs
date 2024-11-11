using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Core.Entities
{
	public class User
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string? MiddleName { get; set; }
		public string LastName { get; set; }
		public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
		public Gender Gender { get; set; } 
		public string Address { get; set; }
		public string ContactNumber { get; set; }
		public DateTime CreatedAt { get; set; }
		public string CreatedBy { get; set; }
		public bool IsActive { get; set; }
		public string? RefreshToken { get; set; }
		public DateTime? RefreshTokenExpiryTime { get; set; }
	}
	public enum Gender
	{
		Male,
		Female,
		Other
	}
}
