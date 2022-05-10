using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace YonderfulApi.Models
{
	public class TaskEmp
	{
		[Key]
		public int Id { get; set; }
		public string Title { get; set; }
		
	}
}