using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YonderfulApi.Data;
using YonderfulApi.Models;

namespace YonderfulApi.Service
{
	public class LocationService : ILocationService
	{
		private readonly DataContext _context;

		public LocationService(DataContext context)
		{
			_context = context;
		}

		public async Task<bool> DeleteLocation(int locationId)
		{
			var location = await _context.Location.FindAsync(locationId);
			if (location == null)
				return false;
			_context.Location.Remove(location);
			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<Location> GetLocation(int locationId)
		{
			var location = await _context.Location.FindAsync(locationId);
			return location;
		}

		public async Task<IList<Location>> GetLocationList()
		{
			var locations = await _context.Location.ToListAsync();
			return locations;
		}

		public async Task<Location> PostLocation(Location newLocation)
		{
			var existingLocation = await LocationExists(newLocation);
			if (existingLocation != null) return existingLocation;

			newLocation.Id = 0;

			_context.Location.Add(newLocation);
			await _context.SaveChangesAsync();
			return newLocation;
		}

		public async Task<Location> PutLocation(Location locationToPut)
		{
			var location = await _context.Location.FindAsync(locationToPut.Id);
			if (location == null)
			{
				return null;
			}
			location.Street = locationToPut.Street;
			location.Address = locationToPut.Address;
			location.City = locationToPut.City;
			location.Province = locationToPut.Province;

			_context.Location.Update(location);
			await _context.SaveChangesAsync();
			return location;
		}

		private async Task<Location> LocationExists(Location newLocation)
		{
			var locations = await _context.Location.ToListAsync();
			foreach (Location loc in locations)
			{
				if (loc.Street == newLocation.Street && loc.Address == newLocation.Address &&
				loc.City == newLocation.City && loc.Province == newLocation.Province)
					return loc;
			}
			return null;
		}

	}
}