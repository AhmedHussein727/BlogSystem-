using AutoMapper;
using Blog.Domain.Entities;
using Blog.Shared.DTOs.CommentsDtos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Sevices.MappingProfiles
{
    public class CommentProfile:Profile
    {
        public CommentProfile()
        {
            CreateMap<Comment, GetCommentsDto>()
                .ForMember(dest => dest.AuthorName,
                 opt => opt.MapFrom(src => src.Author.Name));
            CreateMap<CreateCommentDto, Comment>();

        }
    }
}
