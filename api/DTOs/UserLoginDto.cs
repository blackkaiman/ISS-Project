using System.ComponentModel.DataAnnotations;
using YonderfulApi.DTOs;

namespace api.DTOs
{
	public class UserLoginDto
	{
		public string Name { get; set; }
		[Required]
		[EmailAddress(ErrorMessage = "Invalid Email Address")]
		[RegularExpression("^[a-z]+\\.[a-z]+@tss-yonder\\.com", ErrorMessage = "E-mail is not valid")]
		public string Email { get; set; }

		[Required]
		[StringLength(255, ErrorMessage = "Password must be between 5 and 255 characters!", MinimumLength = 5)]
		public string Password { get; set; }

		public string Token { get; set; }
	}

	public class UserLoginDetailsDto : UserDetailsDto
	{
		public string Token { get; set; }
	}
}