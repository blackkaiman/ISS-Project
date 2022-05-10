using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace YonderfulApi.Models
{
	public class Category
	{
		[Key]
		public int Id { get; set; }
		public string Title { get; set; }
		public int IconId { get; set; }
		public int DefaultBackgroundId { get; set; }
		public bool HasEvents { get; set; }
	}
}