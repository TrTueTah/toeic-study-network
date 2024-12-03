using API.Dtos.Account;
using API.Dtos.CommentDto;
using API.Dtos.ExamDto;
using API.Dtos.ExamSeriesDto;
using API.Dtos.LikeDto;
using API.Dtos.PostDto;
using API.Dtos.QuestionDto;
using API.Dtos.QuestionGroupDto;
using API.Dtos.ResultDto;
using API.Dtos.UserDto;
using API.Models;
using AutoMapper;
using Sprache;

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
 
        //Exam
        CreateMap<CreateExamRequestDto, Exam>();
        CreateMap<Exam, CreateExamRequestDto>();
        CreateMap<Exam, GetAllExamDto>();
        CreateMap<GetAllExamDto, Exam>();
        
        //QuestionGroup
        CreateMap<CreateQuestionGroupDto, QuestionGroup>();
        CreateMap<QuestionGroup, CreateQuestionGroupDto>();
        
        //Question
        CreateMap<CreateQuestionDto, Question>();
        CreateMap<Question, CreateQuestionDto>();

        CreateMap<UserResult, DetailResultDto>();
        CreateMap<UserResult, UserResultDto>();
        CreateMap<UserResult, SubmitResultDto>();
        CreateMap<UserResult, GetAllUserResultDto>();
        CreateMap<DetailResult, DetailResultDto>();
        CreateMap<DetailResult, UserResultDto>();
        CreateMap<DetailResult, SubmitResultDto>();
        CreateMap<GetAllUserResultDto, UserResult>();

        CreateMap<DetailResultDto, UserResult>();
        CreateMap<UserResultDto, UserResult>();
        CreateMap<SubmitResultDto, UserResult>();
        CreateMap<DetailResultDto, DetailResult>();
        CreateMap<UserResultDto, DetailResult>();
        CreateMap<SubmitResultDto, DetailResult>();

        CreateMap<ExamSeries, CreateExamSeriesDto>();
        CreateMap<CreateExamSeriesDto, ExamSeries>();
        CreateMap<ExamSeries, UpdateExamSeriesDto>();
        CreateMap<UpdateExamSeriesDto, ExamSeries>();
        CreateMap<ExamSeries, GetExamSeriesDto>();
        CreateMap<GetExamSeriesDto, ExamSeries>();
    }
}