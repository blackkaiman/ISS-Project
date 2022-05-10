using System.Threading.Tasks;
using api.DTOs;
using YonderfulApi.Models;

namespace api.Service
{
	public interface ILoginService
	{
		Task<User> Login(UserLoginDto userLogin);

		Task<User> VerifyUserCredentials(UserLoginDto userLogin);

		string GenerateJwt(User user, string issuer, string key);
	}
}