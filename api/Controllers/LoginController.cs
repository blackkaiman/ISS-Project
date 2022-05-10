using System.Threading.Tasks;
using api.DTOs;
using api.Service;
using YonderfulApi.Service;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using YonderfulApi.DTOs;
using YonderfulApi.Models;

namespace api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LoginController : ControllerBase
	{
		private readonly IUserService _userService;
		private readonly ILoginService _loginService;
		private readonly IMapper _mapper;
		private readonly IConfiguration _configuration;

		public LoginController(IUserService userService, ILoginService loginService, IMapper mapper, IConfiguration configuration)
		{
			_userService = userService;
			_loginService = loginService;
			_mapper = mapper;
			_configuration = configuration;
		}

		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> Post([FromBody] UserLoginDto loginUser)
		{
			var user = await _loginService.VerifyUserCredentials(loginUser);

			if (user == null) return Unauthorized("Invalid credentials.");

			var userDetails = _mapper.Map<User, UserLoginDetailsDto>(await _userService.GetUserByEmail(loginUser.Email));

			userDetails.Token = _loginService.GenerateJwt(user, _configuration["Jwt:Issuer"], _configuration["Jwt:Key"]);

			return Ok(userDetails);
		}
	}
}