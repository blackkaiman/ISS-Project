using System.ComponentModel.DataAnnotations;

namespace api.DTOs
{
	public class LocationDto
	{
		[Required]
		public int Id { get; set; }
		[Required]
		public string Street { get; set; }
		[Required]
		public string Address { get; set; }
		[Required]
		public string City { get; set; }
		[Required]
		public string Province { get; set; }
	}
}