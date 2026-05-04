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
            CreateMap<BlogPost, BlogPostDto>();
            CreateMap<CreateBlogPostDto, BlogPost>();
            CreateMap<UpdatePostDto, BlogPost>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
