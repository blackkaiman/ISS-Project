using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using api.DTOs;

namespace YonderfulApi.DTOs
{
	public class EventDto
	{
		[Required]
		public int Id { get; set; }
		[Required]
		public int CategoryId { get; set; }
		[Required]
		public int HostId { get; set; }
		[Required]
		public string Title { get; set; }
		[Required]
		public DateTime StartingDate { get; set; } 
		[Required]
		public DateTime EndingDate { get; set; }
		public int MinimumParticipants { get; set; }
		public int MaximumParticipants { get; set; }
		public bool AutoCancel { get; set; }
		public bool AutoJoin { get; set; } 
		[Required]
		public DateTime JoinDeadline { get; set; }
		[Required]
		[Range(0, int.MaxValue, ErrorMessage = "Please enter a positive number for Fee")]
		public int Fee { get; set; }
		[Required]
		public string Description { get; set; }
		[Required]
		public LocationDto EventLocation { get; set; }
		[Required]
		[RegularExpression("^[a-z]+(\\.){0,1}[a-z\\-]*@[a-z]+(\\-){0,1}[a-z]*(\\.){1}com$", ErrorMessage = "E-mail is not valid")]
		public string ContactEmail { get; set; }
		[Required]
		[StringLength(10, ErrorMessage = "Phone number should have 10 digits", MinimumLength =10)]
		[RegularExpression("^[0-9]+", ErrorMessage = "Phone number should be formed out of only digits")]
		public string ContactPhone { get; set; }
		public string Tags { get; set; }
		public string BackgroundImage { get; set; }
	}

	public class DashBoardEventsDto{
		[Required]
		public IList<EventDto> HostedEvents { get; set; }
		[Required]
		public IList<EventDto> JoinedEvents { get; set; }
	}
}