using System.ComponentModel.DataAnnotations;
using System;
namespace YonderfulApi.DTOs
{
	public class FiltersDto
	{
		[Required]
		public DateTime StartingDate { get; set; }
		public DateTime? EndingDate { get; set; }
		public int[] Categories { get; set; }
		public bool? HiddenIfFee { get; set; }
		public bool? HiddenIfStarted { get; set; }
		public int? HostId { get; set; }
		public int? AttendingId { get; set; }
		public string SearchTitle { get; set; }
	}
}
