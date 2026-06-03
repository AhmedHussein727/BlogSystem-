using Blog.Domain.Entities;
using Blog.Services.Abstraction;
using Blog.Shared.DTOs.CategoriesDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Presentation.Controllers
{
    public class CategoriesController:ApiBaseController
    {
        private readonly ICatService _catService;

        public CategoriesController(ICatService catService)
        {
            _catService = catService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CatDTO>>> GetAllCategories()
        {
            var categories =
            await _catService
                .GetCategoriesAsync();

            return Ok(categories);

        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<CatDTO>> CreateCategory( CreateCategoryDto dto)
        {
            var category =
                await _catService
                    .CreateCategoryAsync(dto);

            return CreatedAtAction(
                nameof(GetAllCategories),
                new { id = category.Id },
                category);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CatDTO>>GetCategory(int id)
        {
            var category =
                await _catService.GetCategoryByIdAsync(id);

            if (category is null)
                return NotFound();

            return Ok(category);
        }



        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<CatDTO>> UpdateCategory( int id, UpdateCategoryDto dto)
        {
            var category =
                await _catService
                    .UpdateCategoryAsync(id, dto);

            if (category is null)
                return NotFound();

            return Ok(category);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result =
                await _catService
                    .DeleteCategoryAsync(id);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
