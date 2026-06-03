using AutoMapper;
using Blog.Domain.Entities;
using Blog.Shared.DTOs.CategoriesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Sevices.MappingProfiles
{
    public class CategoriesProfile:Profile
    {
        public CategoriesProfile()
        {
            CreateMap<Category,CatDTO>();
        }
    }
}
