using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using YonderfulApi.Models;

namespace YonderfulApi.DTOs
{
	public class UserDto
	{
		[Required]
		public string Name { get; set; }
		[Required]
		[EmailAddress(ErrorMessage = "Invalid Email Address")]
		[RegularExpression("^([a-z]+)\\.([a-z]+)(@tss-yonder.com)", ErrorMessage = "E-mail is not valid")]
		public string Email { get; set; }
		[Required]
		[StringLength(255, ErrorMessage = "Password must be between 6 and 255 characters!", MinimumLength = 5)]
		public string Password { get; set; }
		public bool IsPresent {get; set; }
	}

	public class UserDetailsDto
	{
		[Required]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		[EmailAddress(ErrorMessage = "Invalid Email Address")]
		[RegularExpression("^([a-z]+)\\.([a-z]+)(@tss-yonder.com)", ErrorMessage = "E-mail is not valid")]
		public string Email { get; set; }
		[Required]
		public Role Role { get; set; }
		[Required]
		public string Position { get; set; }
		[StringLength(10)]
		public string PhoneNo { get; set; }
	}

	public class UserUpdateDto
	{
		[Required]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Position { get; set; }
		[StringLength(10)]
		public string PhoneNo { get; set; }

		[Required]
		public bool IsPresent { get; set; }

	}

	public class UserPresenceDto{
		[Required]
		public int Id { get; set; }
		[Required]
		public bool IsPresent { get; set; }
	}
}