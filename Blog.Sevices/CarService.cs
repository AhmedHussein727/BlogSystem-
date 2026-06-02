using AutoMapper;
using Blog.Domain.Entities;
using Blog.Domain.Interfaces;
using Blog.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Sevices
{
    public class CarService : ICatService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CarService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            var categories=await _unitOfWork.GetRepository<Category,int>().GetAllAsync();
            return _mapper.Map<IEnumerable<Category>>(categories);
        }
    }
}
