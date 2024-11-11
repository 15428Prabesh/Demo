using Microsoft.EntityFrameworkCore;
using SMS.Infrastructure.Data;
using SMS.Core.Entities;
using SMS.Core.Interfaces;

namespace SMS.Infrastructure.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly ApplicationDbContext _context;
		public UserRepository(ApplicationDbContext applicationDbContext) { 
		_context = applicationDbContext;
		}
		public async Task<User> GetUserByUsernameAsync(string username)
		{
			return await _context.user.SingleOrDefaultAsync(u => u.UserName == username);
		}
		public async Task<User> GetUserByEmailAsync(string email)
		{
			return await _context.user.SingleOrDefaultAsync(u => u.Email == email);
		}
		public async Task AddUserAsync(User user)
		{
			await _context.user.AddAsync(user);
			await _context.SaveChangesAsync();
		}		
		public async Task UpdateAsync(User user)
		{
			_context.user.Update(user);
			await _context.SaveChangesAsync();
		}
	}
}
