using System.ComponentModel.DataAnnotations;


namespace YonderfulApi.Models
{
	public class Picture
	{
		[Key]
		public int Id { get; set; }
		public string Content { get; set; }
	}
}