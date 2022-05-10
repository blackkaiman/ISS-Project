using System.Collections.Generic;
using System.Threading.Tasks;
using YonderfulApi.Models;
using System.Drawing;

namespace YonderfulApi.Service
{
	public interface IPictureService
	{
		Task<IList<Picture>> GetPictureList();
		Task<Picture> GetPicture(int pictureId);

		Task<int> GetPictureId(Picture picture);
		Task<Picture> PostPicture(Picture newPicture);

		Task<bool> DeletePicture(int pictureId);

		Task<int> CreatePictureByContent(string pictureContent);

		Task<string> GetPictureContent(string pictureIdStr);
	}
}