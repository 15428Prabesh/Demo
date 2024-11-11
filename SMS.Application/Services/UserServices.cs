using SMS.Application.DTOs;
using SMS.Core.Entities;
using SMS.Core.Interfaces;


namespace SMS.Application.Services
{
	public class UserServices
	{
		private readonly IUserRepository _userRepository;
		private readonly IJwtService _jwtService;
		private readonly ActivityLoggerServices _activityLogger;
		public UserServices(IUserRepository userRepository, IJwtService jwtService, ActivityLoggerServices activityLogger)
		{
			_userRepository = userRepository;
			_jwtService = jwtService;
			_activityLogger = activityLogger;
		}
		public async Task<bool> RegisterUserAsync(string username, string firstname, string lastname,string middlename, string password, string email,string address,Gender gender,string contactnumber)
		{
			try {
				var existingUser = await _userRepository.GetUserByUsernameAsync(username);
				if (existingUser != null) return false;
				var existingUserByEmail = await _userRepository.GetUserByEmailAsync(email);
				if (existingUserByEmail != null)
					return false;
				var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
				var user = new User
				{
					UserName = username,
					FirstName = firstname,
					MiddleName = middlename,
					LastName = lastname,
					Email = email,
					Password = hashedPassword,
					Address = address,
					Gender = gender,
					CreatedAt = DateTime.UtcNow,
					CreatedBy = "admin",
					ContactNumber = contactnumber,
					IsActive = true,
					RefreshToken = null,
					RefreshTokenExpiryTime=null,
				};
				await _userRepository.AddUserAsync(user);
				var userId = user.Id;
				await _activityLogger.LogUserAction(userId, "User Registered", "User registered successfully.");
				return true;
			}
			catch (Exception ex) {
				Console.WriteLine($"Error during user registration: {ex.Message}");
				throw;
			}
		}
		public async Task<string> Login(UserDTO userDTO)
		{
			try
			{
				var user = await _userRepository.GetUserByEmailAsync(userDTO.Email);
				if (user == null || !BCrypt.Net.BCrypt.Verify(userDTO.Password, user.Password))
				{
					throw new UnauthorizedAccessException("Invalid credentials");
				}
				var token = _jwtService.GenerateJwtToken(user);
				var refreshToken = GenerateRefreshToken();
				user.RefreshToken = refreshToken;
				user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7); 

				await _userRepository.UpdateAsync(user);

				//await LogUserAction(user.Id, "User Logged In", "User logged in successfully.");
				await _activityLogger.LogUserAction(user.Id, "User Logged In", "User logged in successfully.");

				return token;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error occurred: {ex.Message}");
				throw;
			}	
		}
		private string GenerateRefreshToken()
		{
			return Guid.NewGuid().ToString(); // Simple refresh token generation logic. Consider using a more secure approach.
		}
		public async Task<string> RefreshToken(string email, string refreshToken)
		{
			try
			{
				var user = await _userRepository.GetUserByEmailAsync(email);
				if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
				{
					throw new UnauthorizedAccessException("Invalid refresh token.");
				}
				var newJwtToken = _jwtService.GenerateJwtToken(user);

				var newRefreshToken = GenerateRefreshToken();
				user.RefreshToken = newRefreshToken;
				user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7); // Reset expiry time

				await _userRepository.UpdateAsync(user);
				return newJwtToken;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error occurred: {ex.Message}");
				throw;
			}
		}

		public async Task Logout(string email)
		{
			try
			{
				var user = await _userRepository.GetUserByEmailAsync(email);
				if (user != null)
				{
					user.RefreshToken = null;

					await _userRepository.UpdateAsync(user);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error occurred: {ex.Message}");
				throw;
			}			
		}

	}
}




