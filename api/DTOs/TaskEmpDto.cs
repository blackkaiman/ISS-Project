using System.ComponentModel.DataAnnotations;
using System.IO;
namespace YonderfulApi.DTOs
{
	public class TaskEmpDto
	{
		[Required]
		public int Id { get; set; }
		[Required]
		public string Title { get; set; }
	}
}