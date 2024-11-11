using API.Dtos.Account;
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
        CreateMap<User, UserNameDto>();
        CreateMap<UserNameDto, User>();
        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>();

        //Comment
        CreateMap<Comment, CommentDto>();
        CreateMap<CommentDto, Comment>();
        CreateMap<Comment, CreateCommentDto>();
        CreateMap<CreateCommentDto, Comment>();

        //Account
        CreateMap<User, UserLoginResponseDto>();
        CreateMap<UserLoginResponseDto, User>();
        CreateMap<User, UserRegisterResponseDto>();
        CreateMap<UserRegisterResponseDto, User>();
    }
}