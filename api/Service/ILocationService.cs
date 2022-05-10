using System.Collections.Generic;
using System.Threading.Tasks;
using YonderfulApi.Models;

namespace YonderfulApi.Service
{
	public interface ILocationService
	{
		Task<Location> GetLocation(int locationId);
		Task<IList<Location>> GetLocationList();
		Task<Location> PostLocation(Location newLocation);
		Task<Location> PutLocation(Location locationToPut);
		Task<bool> DeleteLocation(int locationId);
	}
}