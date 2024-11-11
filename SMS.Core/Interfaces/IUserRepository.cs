using SMS.Core.Entities;

namespace SMS.Core.Interfaces
{
	public interface IUserRepository
	{
		Task<User> GetUserByUsernameAsync(string username);
		Task<User> GetUserByEmailAsync(string email);
		Task AddUserAsync(User user);

		Task UpdateAsync(User user);
	}
	
}
