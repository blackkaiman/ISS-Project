using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YonderfulApi.Models
{
	public class User
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
		public Role Role { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string Position { get; set; } = "Employee";
		[StringLength(10)]
		public string PhoneNo { get; set; } = "";
		public bool IsPresent { get; set; }
	}
}