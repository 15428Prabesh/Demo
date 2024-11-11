using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SMS.Application.DTOs;
using SMS.Application.Services;
using SMS.Core.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace SMS.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AuthController : ControllerBase
	{
		private readonly UserServices _userServices;
		private readonly CrudServices<User> _crudServices;
		private readonly ActivityLoggerServices _activityLogger;
		public AuthController(UserServices userServices, CrudServices<User> crudServices,ActivityLoggerServices activityLoggerServices)
		{
			_userServices = userServices;
			_crudServices = crudServices;
			_activityLogger = activityLoggerServices;
		}
		[HttpGet("GetAllUser")]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				var users = await _crudServices.GetAllAsync();
				return Ok(users);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error occurred: {ex.Message}");
				return StatusCode(500, "An unexpected error occurred. Please try again later.");
			}	
		}
		[HttpGet("GetUserById")]
		public async Task<IActionResult> GetUserById(int UserId)
		{
			try
			{
				var users = await _crudServices.GetByIdAsync(UserId);
				if (users == null) {
					return BadRequest("User Not Found!");
				}
				return Ok(users);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error occurred: {ex.Message}");
				return StatusCode(500, "An unexpected error occurred. Please try again later.");
			}
		}
		[HttpPost("signup")]
		public async Task<IActionResult> SignUp([FromBody] SignupDTO signupDTO)
		{
			try
			{
				if (signupDTO.Password != signupDTO.ConfirmPassword) { 
					return BadRequest("Passwords do not match");
				}
				if (!IsValidGmail(signupDTO.Email))
				{
					return BadRequest("Please provide a valid Gmail address.");
				}
				if (!HasSpecialCharacter(signupDTO.Password))
				{
					return BadRequest("Password must contain at least one special character.");
				}
				var success = await _userServices.RegisterUserAsync(signupDTO.Username,signupDTO.Firstname,signupDTO.Lastname,signupDTO.Middlename,signupDTO.Password,signupDTO.Email,signupDTO.Address,signupDTO.Gender,signupDTO.ContactNumber);

				if (success)
					return Ok("User registered successfully!");

				return BadRequest("User is already taken.");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error occurred: {ex.Message}");
				return StatusCode(500, "An unexpected error occurred. Please try again later.");
			}
			
		}
		[HttpPost("login")]
		public async Task<IActionResult> LogIn(UserDTO userDTO)
		{
			try
			{
				if (!IsValidGmail(userDTO.Email) && string.IsNullOrEmpty(userDTO.Email))
				{
					return BadRequest("Invalid Email");	
				}
				if (!HasSpecialCharacter(userDTO.Password) && string.IsNullOrEmpty(userDTO.Password)) {
					return BadRequest("Invalid Password");
				}
			    var token = await _userServices.Login(userDTO);
				return Ok(new { token });
			}
			catch (UnauthorizedAccessException ex)
			{
				Console.WriteLine($"Unauthorized access:{ex.Message}");
				return Unauthorized("Invalid credentials");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error occurred: {ex.Message}");
				return StatusCode(500, "An unexpected error occurred. Please try again later.");
			}
		}
		[HttpPost("refresh-token")]
		public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDTO refreshTokenDTO)
		{
			try
			{
				var response = await _userServices.RefreshToken(refreshTokenDTO.Email, refreshTokenDTO.RefreshToken);
				return Ok(new { Token = response });
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error occurred: {ex.Message}");
				return StatusCode(500, "An unexpected error occurred. Please try again later.");
			}
			
		}

		[HttpPost("logout")]
		public async Task<IActionResult> Logout([FromBody] LogoutDTO logoutDTO)
		{
			try
			{
				await _userServices.Logout(logoutDTO.Email);
				return Ok("Logged out successfully");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error occurred: {ex.Message}");
				return StatusCode(500, "An unexpected error occurred. Please try again later.");
			}
			
		}
		//[HttpGet("user-logs/{userId}")]
		//public async Task<IActionResult> GetUserLogs(string userId)
		//{
		//	try
		//	{
		//		var logs = await _activityLogger.GetUserLogsAsync(userId);
		//		return Ok(logs);
		//	}
		//	catch (Exception ex)
		//	{
		//		Console.WriteLine($"Error occurred: {ex.Message}");
		//		return StatusCode(500, "An unexpected error occurred. Please try again later.");
		//	}
		//}
		private bool IsValidGmail(string email)
		{
			return Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@gmail\.com$", RegexOptions.IgnoreCase);
		}
		private bool HasSpecialCharacter(string password)
		{
			return Regex.IsMatch(password, @"[!@#$%^&*(),.?""':{}|<>]");
		}
	}
}
