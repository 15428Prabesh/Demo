using SMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Application.DTOs
{
	public class UserDTO
	{
		public string Email { get; set; }
		public string Password { get; set; }
	}
	public class SignupDTO
	{
		public string Username { get; set; }
		public string Firstname { get; set; }
		public string Middlename { get; set; }
		public string Lastname { get; set; }
		public string Password { get; set; }
		public string ConfirmPassword { get; set; }
		public string Email { get; set; }
		public string Address { get; set; }
		public Gender Gender { get; set; }
		public string ContactNumber { get; set; }		
	}
	public class RefreshTokenDTO
	{
		public string Email { get; set; }
		public string RefreshToken { get; set; }
	}
	public class LogoutDTO
	{
		public string Email { get; set; }
	}
}
