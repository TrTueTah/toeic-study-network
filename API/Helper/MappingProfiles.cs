using API.Dtos.CommentDto;
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
        //Post
        CreateMap<Post, PostDto>();
        CreateMap<PostDto, Post>();
        CreateMap<Post, CreatePostDto>();
        CreateMap<CreatePostDto, Post>();
        
        //Like
        CreateMap<Like, LikeDto>();
        CreateMap<LikeDto, Like>();
        
        //User
        CreateMap<AppUser, UserNameDto>();
        CreateMap<UserNameDto, AppUser>();
        CreateMap<AppUser, UserDto>();
        CreateMap<UserDto, AppUser>();
        
        //Comment
        CreateMap<Comment, CommentDto>();
        CreateMap<CommentDto, Comment>();
        CreateMap<Comment, CreateCommentDto>();
        CreateMap<CreateCommentDto, Comment>();
    }
}