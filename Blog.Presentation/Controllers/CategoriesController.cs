using Blog.Domain.Entities;
using Blog.Services.Abstraction;
using Blog.Shared.DTOs.CategoriesDTO;
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
    }
}
