using AutoMapper;
using ETaskManagement.Contract.Login.Request;
using ETaskManagement.Contract.Login.Response;
using ETaskManagement.Contract.Task.Request;
using ETaskManagement.Contract.Task.Response;
using ETaskManagement.Contract.User.Request;
using ETaskManagement.Contract.User.Response;
using ETaskManagement.Domain.Token;
using ETaskManagement.Domain.User;
using Task = ETaskManagement.Domain.Task.Task;

namespace ETaskManagement.Api.Common.Mapper;

public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<User, CreateUser>().ReverseMap();
        CreateMap<User, UpdateUser>().ReverseMap();
        CreateMap<User, UserResponse>().ReverseMap();
        CreateMap<User, LoginRequest>().ReverseMap();
        CreateMap<Token, LoginResponse>().ReverseMap();
        CreateMap<Task, CreateTask>().ReverseMap();
        CreateMap<Task, UpdateTask>().ReverseMap();
        CreateMap<Task, DetailTask>().ReverseMap();
        CreateMap<Task, ResponseTask>().ReverseMap();
    }
}