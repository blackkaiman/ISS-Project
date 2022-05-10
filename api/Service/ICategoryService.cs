using System.Collections.Generic;
using System.Threading.Tasks;
using YonderfulApi.DTOs;
using YonderfulApi.Models;

namespace YonderfulApi.Service
{
	public interface ICategoryService
	{
		Task<IList<Category>> GetCategoryList();
		Task<Category> GetCategory(int categoryId);
		Task<Category> PostCategory(Category newCategory);

		Task<Category> PutCategory(int categoryId, Category categoryToPut);

		Task<bool> DeleteCategory(int categoryId);

		Task<CategoryDto> TransformCategoryDtoForOutput(CategoryDto categoryDto);

		Task<IList<CategoryDto>> TransformCategoryDtoListForOutput(IList<CategoryDto> categoryList);

		Task<Category> CreateCategory(CategoryDto categoryDto);
	}
}