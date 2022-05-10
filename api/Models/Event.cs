using System;
using System.ComponentModel.DataAnnotations;

namespace YonderfulApi.Models
{
	public class Event
	{
		[Key]
		public int Id { get; set; }
		public int CategoryId { get; set; }
		public int HostId { get; set; }
		public string Title { get; set; }
		public DateTime StartingDate { get; set; }
		public DateTime EndingDate { get; set; }
		public int MinimumParticipants { get; set; }
		public int MaximumParticipants { get; set; }
		public bool AutoCancel { get; set; }
		public bool AutoJoin { get; set; }
		public DateTime JoinDeadline { get; set; }
		public int Fee { get; set; }
		public string Description { get; set; }
		public Location EventLocation { get; set; }
		public string ContactEmail { get; set; }
		public string ContactPhone { get; set; }
		public string Tags { get; set; }
		public int BackgroundId { get; set; }
	}
}