using Blog.Domain.Entities;
using Blog.Shared.DTOs.CategoriesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Services.Abstraction
{
    public interface ICatService
    {
        Task<IEnumerable<CatDTO>> GetCategoriesAsync();
        Task<CatDTO> CreateCategoryAsync(CreateCategoryDto dto);
        Task<CatDTO?> GetCategoryByIdAsync(int id);
        Task<CatDTO?> UpdateCategoryAsync(int id, UpdateCategoryDto dto);

        Task<bool> DeleteCategoryAsync(int id);
    }
}
