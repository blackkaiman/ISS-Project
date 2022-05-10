using System.Collections.Generic;
using System.Threading.Tasks;
using YonderfulApi.Models;
using System.Drawing;
using YonderfulApi.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace YonderfulApi.Service
{
	public class PictureService : IPictureService
	{
		private readonly DataContext _context;
		public PictureService(DataContext context)
		{
			_context = context;
		}
		public async Task<Picture> GetPicture(int pictureId)
		{
			var picture = await _context.Pictures.FindAsync(pictureId);
			return picture;
		}

		public async Task<IList<Picture>> GetPictureList()
		{
			var pictureList = await _context.Pictures.ToListAsync();
			return pictureList;
		}

		public async Task<int> GetPictureId(Picture picture)
		{
			var pictureList = await GetPictureList();
			foreach (Picture pictureInList in pictureList)
			{
				if (picture.Content.CompareTo(pictureInList.Content) == 0)
				{
					return pictureInList.Id;
				}
			}
			return 0;
		}
		public async Task<Picture> PostPicture(Picture newPicture)
		{
			_context.Pictures.Add(newPicture);
			await _context.SaveChangesAsync();
			return newPicture;
		}

		public async Task<bool> DeletePicture(int pictureId)
		{
			var picture = await _context.Pictures.FindAsync(pictureId);
			if (picture == null)
			{
				return false;
			}
			_context.Pictures.Remove(picture);
			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<int> CreatePictureByContent(string pictureContent)
		{
			var newPicture = new Picture
			{
				Content = pictureContent
			};
			var existingPictureId = await GetPictureId(newPicture);
			if (existingPictureId == 0)
			{
				var auxPicture = await PostPicture(newPicture);
				return auxPicture.Id;
			}
			return existingPictureId;
		}

		public async Task<string> GetPictureContent(string pictureIdStr)
		{
			var iconPicture = await GetPicture(Int32.Parse(pictureIdStr));
			return iconPicture.Content;
		}
	}
}