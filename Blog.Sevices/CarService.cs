using AutoMapper;
using Blog.Domain.Entities;
using Blog.Domain.Interfaces;
using Blog.Services.Abstraction;
using Blog.Shared.DTOs.CategoriesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Sevices
{
    public class CatService : ICatService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CatService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CatDTO> CreateCategoryAsync(
             CreateCategoryDto dto)
        {
            var category = new Category
            {
                Name = dto.Name
            };

            await _unitOfWork
                .GetRepository<Category, int>()
                .AddAsync(category);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<CatDTO>(category);
        }

        public async Task<IEnumerable<CatDTO>> GetCategoriesAsync()
        {
            var categories=await _unitOfWork.GetRepository<Category,int>().GetAllAsync();
            return _mapper.Map<IEnumerable<CatDTO>>(categories);
        }

        public async Task<CatDTO?> GetCategoryByIdAsync(int id)
        {
            var category =
                await _unitOfWork
                .GetRepository<Category, int>()
                .GetByIdAsync(id);

            if (category is null)
                return null;

            return _mapper.Map<CatDTO>(category);
        }

        public async Task<CatDTO?> UpdateCategoryAsync(
    int id,
    UpdateCategoryDto dto)
        {
            var category =
                await _unitOfWork
                .GetRepository<Category, int>()
                .GetByIdAsync(id);

            if (category is null)
                return null;

            category.Name = dto.Name;

            _unitOfWork
                .GetRepository<Category, int>()
                .Update(category);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<CatDTO>(category);
        }


        public async Task<bool> DeleteCategoryAsync( int id)
        {
            var category =
                await _unitOfWork
                .GetRepository<Category, int>()
                .GetByIdAsync(id);

            if (category is null)
                return false;

            _unitOfWork
                .GetRepository<Category, int>()
                .Delete(category);

            await _unitOfWork.SaveChangesAsync();

            return true;
        }


    }
}
