using System.ComponentModel.DataAnnotations;

namespace YonderfulApi.Models
{
	public class Location
	{
		[Key]
		public int Id { get; set; }
		public string Street { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string Province { get; set; }
	}
}