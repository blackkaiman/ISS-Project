using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using YonderfulApi.DTOs;
using YonderfulApi.Models;
using YonderfulApi.Service;

namespace api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EventController : ControllerBase
	{
		private readonly IEventService _eventService;
		private readonly IUserService _userService;
		private readonly IAttendanceService _attendanceService;
		private readonly ICategoryService _categoryService;
		private readonly IPictureService _pictureService;
		private readonly IMapper _mapper;

		public EventController(IEventService eventService, ICategoryService categoryService,
								IUserService userService, IAttendanceService attendanceService,
								IPictureService pictureService, IMapper mapper)
		{
			_eventService = eventService;
			_attendanceService = attendanceService;
			_categoryService = categoryService;
			_userService = userService;
			_pictureService = pictureService;
			_mapper = mapper;
		}

		private async void AutoJoin(Event createdEvent)
		{
			var attendanceDto = new AttendanceDto();
			attendanceDto.EventId = createdEvent.Id;
			attendanceDto.UserId = createdEvent.HostId;
			attendanceDto.JoiningDate = DateTime.UtcNow.ToLocalTime();
			var newAttendance = _mapper.Map<Attendance>(attendanceDto);
			await _attendanceService.CreateAttendance(_mapper.Map<AttendanceDto, Attendance>(attendanceDto));
		}

		[HttpGet("{eventId}")]
		public async Task<IActionResult> GetEvent(int eventId)
		{
			var myEvent = await _eventService.GetEvent(eventId);
			if (myEvent == null)
			{
				return NotFound("Event with id given not found");
			}
			var eventDto = await _eventService.TransformEventDtoForOutput(_mapper.Map<EventDto>(myEvent));
			return Ok(eventDto);
		}

		[HttpGet("[action]/{userId}")]
		public async Task<IActionResult> GetJoinedEventsForUser(int userId)
		{
			var joinedEvents = await _eventService.GetJoinedEventsForUser(userId);
			return Ok(_mapper.Map<IList<EventDto>>(joinedEvents));
		}

		[HttpGet("[action]/{userId}")]
		public async Task<IActionResult> GetDashboardEvents(int userId)
		{
			DashBoardEventsDto dashBoardEventsDto = new DashBoardEventsDto();

			var hostedEvents = await _eventService.GetHostedEvents(userId);
			var joinedEvents = await _eventService.GetFutureJoinedEvents(userId);

			dashBoardEventsDto.HostedEvents = await _eventService.TransformEventDtoListForOutput(_mapper.Map<IList<EventDto>>(hostedEvents));
			dashBoardEventsDto.JoinedEvents = await _eventService.TransformEventDtoListForOutput(_mapper.Map<IList<EventDto>>(joinedEvents));

			return Ok(dashBoardEventsDto);
		}

		[HttpGet]
		public async Task<IActionResult> GetEvents()
		{
			var myEventList = await _eventService.GetEventList();
			var eventDtoList = await _eventService.TransformEventDtoListForOutput(_mapper.Map<IList<EventDto>>(myEventList));
			return Ok(eventDtoList);
		}

		[HttpGet("getFutureEvents")]
		public async Task<IActionResult> GetFutureEvents()
		{
			var myEventList = await _eventService.GetFutureEventList();
			var eventDtoList = _eventService.TransformEventDtoListForOutput(_mapper.Map<IList<EventDto>>(myEventList));
			return Ok(eventDtoList);
		}

		[HttpPost("getFilteredEvents")]
		public async Task<IActionResult> GetFilteredEvents(FiltersDto filtersDto)
		{
			var myEventList = await _eventService.GetFilteredEvents(filtersDto);
			var eventDtoList = _eventService.TransformEventDtoListForOutput(_mapper.Map<IList<EventDto>>(myEventList));
			if (eventDtoList == null)
			{
				return NotFound();
			}
			return Ok(eventDtoList);
		}

		[HttpPost("getNotEndedEvents")]
		public async Task<IActionResult> GetNotEndedEvents(FiltersDto filtersDto){
			var eventList = await _eventService.FilterNotEndedEvents(filtersDto);
			var eventDtoList = _eventService.TransformEventDtoListForOutput(_mapper.Map<IList<EventDto>>(eventList));
			if (eventDtoList == null)
			{
				return NotFound();
			}
			return Ok(eventDtoList);
		}

		[HttpPost]
		public async Task<IActionResult> PostEvent(EventDto eventDto)
		{
			eventDto = await CheckBackgroundImage(eventDto);
			var newEvent = await _eventService.CreateEvent(eventDto);

			var isValid = EventValidations(newEvent);

			if (newEvent.MinimumParticipants == 0)
				newEvent.AutoCancel = false;

			var createdEvent = await _eventService.PostEvent(newEvent);

			if (newEvent.AutoJoin)
			{
				AutoJoin(createdEvent);
			}

			if (createdEvent == null)
			{
				return BadRequest();
			}
			return Created(nameof(GetEvent), _mapper.Map<EventDto>(createdEvent));
		}

		[HttpDelete("{eventId}")]
		public async Task<IActionResult> DeleteEvent(int eventId)
		{
			var deletedEvent = await _eventService.DeleteEvent(eventId);
			return deletedEvent ? Ok() : BadRequest();
		}

		[HttpPut]
		public async Task<IActionResult> PutEvent(EventDto newEventDto)
		{
			var newEvent = await _eventService.CreateEvent(newEventDto);
			var putEvent = await _eventService.PutEvent(newEvent);

			if (newEvent.AutoJoin)
			{
				AutoJoin(putEvent);
			}

			if (putEvent == null)
			{
				return BadRequest();
			}
			return Ok(putEvent);
		}

		private async Task<Tuple<bool, string>> EventValidations(Event newEvent)
		{
			var existingCategory = await _categoryService.GetCategory(newEvent.CategoryId);
			var existingUser = await _userService.GetUserById(newEvent.HostId);

			if (existingCategory == null)
				return new Tuple<bool, string>(false, "Category doesn't exist.");
			if (existingUser == null)
				return new Tuple<bool, string>(false, "Host doesn't exist.");
			if (newEvent.StartingDate >= newEvent.EndingDate)
				return new Tuple<bool, string>(false, "Starting date should be before ending date.");
			if (newEvent.StartingDate < newEvent.JoinDeadline)
				return new Tuple<bool, string>(false, "Joining deadline should be before starting date.");
			if (newEvent.MinimumParticipants > newEvent.MaximumParticipants)
				return new Tuple<bool, string>(false, "Minimum number of participants should be lower than the maximum one.");
			if (newEvent.Tags.Split("*").Length > 5)
				return new Tuple<bool, string>(false, "Maximum 5 tags are allowed");

			return new Tuple<bool, string>(true, "");
		}

		private async Task<EventDto> CheckBackgroundImage(EventDto eventDto)
		{
			if (string.IsNullOrEmpty(eventDto.BackgroundImage))
			{
				var eventCategory = await _categoryService.GetCategory(eventDto.CategoryId);
				eventDto.BackgroundImage = await _pictureService.GetPictureContent(eventCategory.DefaultBackgroundId.ToString());
				return eventDto;
			}
			return eventDto;
		}
	}
}