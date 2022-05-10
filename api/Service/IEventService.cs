using System.Collections.Generic;
using System.Threading.Tasks;
using YonderfulApi.DTOs;
using YonderfulApi.Models;

namespace YonderfulApi.Service
{
	public interface IEventService
	{
		Task<IList<Event>> GetEventList();
		Task<Event> GetEvent(int id);
		Task<Event> PostEvent(Event newEvent);
		Task<Event> PutEvent(Event eventToPut);
		Task<bool> DeleteEvent(int eventID);
		Task<EventDto> TransformEventDtoForOutput(EventDto eventDto);
		Task<IList<EventDto>> TransformEventDtoListForOutput(IList<EventDto> eventDtoList);
		Task<Event> CreateEvent(EventDto eventDto);
		Task<IList<Event>> GetFutureEventList();
		Task<IList<Event>> GetFilteredEvents(FiltersDto filtersDto);
		Task<IList<Event>> GetHostedEvents(int hostId);
		Task<IList<Event>> GetFutureJoinedEvents(int hostId);
		Task<IList<Event>> GetJoinedEventsForUser(int userId);
		Task<IList<Event>> FilterNotEndedEvents(FiltersDto filtersDto);
	}
}