using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using YonderfulApi.DTOs;
using YonderfulApi.Models;
using YonderfulApi.Service;

namespace YonderfulApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;
		private readonly IMapper _mapper;

		public UserController(IUserService userService, IMapper mapper)
		{
			_userService = userService;
			_mapper = mapper;
		}

		[HttpGet("Id/{userId}")]
		public async Task<IActionResult> GetUserById(int userId)
		{
			var user = await _userService.GetUserById(userId);
			if (user == null)
			{
				return NotFound("No user with given id!");
			}
			return Ok(_mapper.Map<UserDetailsDto>(user));
		}

		[HttpGet("email/{email}")]
		public async Task<IActionResult> GetUserByEmail(string email)
		{
			var user = await _userService.GetUserByEmail(email);
			if (user == null)
			{
				return NotFound("No user with given id!");
			}
			return Ok(_mapper.Map<UserDetailsDto>(user));
		}
		[Route("GetPresent")]
		[HttpGet]
		public async Task<IActionResult> GetUserList()
		{
			var userList = await _userService.GetPresentEmployees();
			if (userList == null)
			{
				return NotFound();
			}
			return Ok(_mapper.Map<IList<UserDetailsDto>>(userList));
		}


		[HttpPost]
		[Produces("application/json")]
		[ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		public async Task<IActionResult> PostUser(UserDto user)
		{
			var newUser = _mapper.Map<UserDto, User>(user);
			var postUser = await _userService.PostUser(newUser);

			if (postUser == null) return BadRequest("User with given email already exists.");
			return Created(nameof(GetUserById), postUser);
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteUser(int userId)
		{
			var removedUser = await _userService.DeleteUser(userId);
			return removedUser ? Ok("User deleted succesfully!") : BadRequest("User with given id does not exist!");
		}

		[HttpPut]
		public async Task<IActionResult> PutUser(UserUpdateDto user)
		{
			var newUser = _mapper.Map<UserUpdateDto, User>(user);
			var putUser = await _userService.PutUser(newUser);

			if (putUser == null) return BadRequest("User with given ID doesn't exist.");
			return Created(nameof(GetUserById), putUser);
		}

		[Route("updatePresence")]
		[HttpPut]
		public async Task<IActionResult> PutUserPresence(UserPresenceDto user)
		{
			var putUser = await _userService.PutUserPresence(user.Id,user.IsPresent);

			if (putUser == null) return BadRequest("User with given ID doesn't exist.");
			return Created(nameof(GetUserById), putUser);
		}
	}
}
