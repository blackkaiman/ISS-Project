using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using api.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using YonderfulApi.Data;
using YonderfulApi.Models;
using YonderfulApi.Service;

namespace api.Service
{
	public class LoginService : ILoginService
	{
		private readonly DataContext _dataContext;
		private HashingManager _hashingManager;

		public LoginService(DataContext dataContext)
		{
			_dataContext = dataContext;
			_hashingManager = new();
		}

		public Task<User> Login(UserLoginDto userLogin)
		{
			throw new System.NotImplementedException();
		}

		public async Task<User> VerifyUserCredentials(UserLoginDto userLogin)
		{
			var user = await _dataContext.Users.FirstOrDefaultAsync(user => user.Email == userLogin.Email);

			if (user == null || !_hashingManager.Verify(userLogin.Password, user.Password)) return null;

			return user;
		}

		public string GenerateJwt(User user, string issuer, string key)
		{
			var securityKey = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(key));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			var claims = new[] {
		new Claim(JwtRegisteredClaimNames.Sub, user.Name),
		new Claim(JwtRegisteredClaimNames.Email, user.Email),
		new Claim("UserRole", ((int)user.Role).ToString()),
		new Claim("UserID", user.Id.ToString()),
		new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
	  };

			var token = new JwtSecurityToken(
			  issuer: issuer,
			  audience: issuer,
			  claims: claims,
			  expires: DateTime.Now.AddHours(72),
			  signingCredentials: credentials
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
