using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using YonderfulApi.DTOs;
using YonderfulApi.Service;
using YonderfulApi.Models;
using System;

namespace YonderfulApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private readonly ICategoryService _categoryService;
		private readonly IMapper _mapper;

		public CategoryController(ICategoryService categoryService, IMapper mapper)
		{
			_categoryService = categoryService;
			_mapper = mapper;
		}

		[HttpGet("{categoryId}")]
		public async Task<IActionResult> GetCategory(int categoryId)
		{
			var category = await _categoryService.GetCategory(categoryId);
			if (category == null)
			{
				return NotFound();
			}
			var categoryDto = _categoryService.TransformCategoryDtoForOutput(_mapper.Map<CategoryDto>(category));
			return Ok(categoryDto);
		}

		[HttpGet]
		public async Task<IActionResult> GetCategoryList()
		{
			var categoryList = await _categoryService.GetCategoryList();
			if (categoryList == null)
			{
				return NotFound();
			}
			var categoryDtoList = _mapper.Map<IList<CategoryDto>>(categoryList);
			return Ok(_categoryService.TransformCategoryDtoListForOutput(categoryDtoList));
		}

		[HttpPost]
		public async Task<IActionResult> PostCategory(CategoryDto category)
		{
			Category newCategory = await _categoryService.CreateCategory(category);

			var createdCategory = await _categoryService.PostCategory(newCategory);
			if (createdCategory == null)
			{
				return BadRequest("Category already exists");
			}
			return Created(nameof(GetCategory), _mapper.Map<CategoryDto>(createdCategory));
		}

		[HttpDelete]
		public async Task<ActionResult> DeleteCategory(int categoryId)
		{
			var removedCategory = await _categoryService.DeleteCategory(categoryId);
			return removedCategory ? Ok() : BadRequest();
		}

		[HttpPut]
		public async Task<ActionResult> PutCategory(CategoryDto updatedCategory)
		{
			Category categoryToPut = await _categoryService.CreateCategory(updatedCategory);

			var newCategory = await _categoryService.PutCategory(updatedCategory.Id, categoryToPut);
			if (newCategory == null)
			{
				return BadRequest();
			}
			return Created(nameof(GetCategory), _mapper.Map<CategoryDto>(newCategory));
		}
	}
}