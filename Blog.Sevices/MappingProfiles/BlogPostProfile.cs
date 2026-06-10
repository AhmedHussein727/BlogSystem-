using AutoMapper;
using Blog.Domain.Entities;
using Blog.Shared.DTOs.PostsDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Sevices.MappingProfiles
{
    public class BlogPostProfile:Profile
    {
        public BlogPostProfile()
        {
            CreateMap<BlogPost, BlogPostDto>()
            .ForMember(
                dest => dest.CategoryName,
                opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(
                dest => dest.AuthorName,
                opt => opt.MapFrom(src => src.Author.Name));
            CreateMap<CreateBlogPostDto, BlogPost>();
            CreateMap<UpdatePostDto, BlogPost>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
