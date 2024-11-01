using API.Dtos.LikeDto;
using API.Dtos.PostDto;
using API.Dtos.UserDto;
using API.Models;
using AutoMapper;

namespace API.Helper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Post, PostDto>();
        CreateMap<PostDto, Post>();
        CreateMap<Post, CreatePostDto>();
        CreateMap<CreatePostDto, Post>();
        CreateMap<Like, LikeDto>();
        CreateMap<LikeDto, Like>();
        CreateMap<AppUser, UserNameDto>();
        CreateMap<UserNameDto, AppUser>();
        CreateMap<AppUser, UserDto>();
        CreateMap<UserDto, AppUser>();
    }
}