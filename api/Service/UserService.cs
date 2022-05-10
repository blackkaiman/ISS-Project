using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YonderfulApi.Models;
using Microsoft.EntityFrameworkCore;
using YonderfulApi.Data;

namespace YonderfulApi.Service
{
	public class UserService : IUserService
	{
		private HashingManager hashing = new HashingManager();
		private readonly DataContext _context;
		public UserService(DataContext context)
		{
			_context = context;
		}

		public async Task<User> GetUserById(int userId)
		{
			var user = await _context.Users.FindAsync(userId);
			return user;
		}

		public async Task<User> GetUserByEmail(string email)
		{
			var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
			return user;
		}

		public async Task<IList<User>> GetUserList()
		{
			var userList = await _context.Users.ToListAsync();
			return userList;
		}

		public async Task<User> PostUser(User user)
		{
			if (await UserExists(user.Email)) return null;

			user.Password = hashing.HashToString(user.Password);
			_context.Users.Add(user);
			await _context.SaveChangesAsync();
			return user;
		}

		public async Task<bool> DeleteUser(int id)
		{
			var user = await _context.Users.FindAsync(id);
			if (user == null) return false;

			_context.Users.Remove(user);
			return await _context.SaveChangesAsync() > 0;
		}

		private async Task<bool> UserExists(string email)
		{
			return await _context.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower());
		}

		public async Task<User> PutUser(User user)
		{
			var existingUser = await _context.Users.FindAsync(user.Id);
			if (existingUser == null) return null;

			existingUser.Name = user.Name;
			existingUser.Position = user.Position;
			existingUser.PhoneNo = user.PhoneNo;

			_context.Users.Update(existingUser);
			await _context.SaveChangesAsync();
			return existingUser;
		}
		public async Task<IList<User>> GetPresentEmployees(){
			var employees = await _context.Users.Where(e=> e.IsPresent == true && e.Role == 0).ToListAsync();
			return employees;
		}

		public async Task<User> PutUserPresence(int id, bool isPresent){
			var existingUser = await _context.Users.FindAsync(id);
			if (existingUser == null) return null;

			existingUser.IsPresent=isPresent;
			_context.Users.Update(existingUser);
			await _context.SaveChangesAsync();
			return existingUser;
		}
	}
}