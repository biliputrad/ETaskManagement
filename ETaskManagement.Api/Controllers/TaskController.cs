using AutoMapper;
using ETaskManagement.Application.Task;
using ETaskManagement.Application.UserIdentity;
using ETaskManagement.Contract.Common.Response;
using ETaskManagement.Contract.Task.Request;
using ETaskManagement.Contract.Task.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Task = ETaskManagement.Domain.Task.Task;

namespace ETaskManagement.Api.Controllers;

[Route("task")]
[Authorize]
public class TaskController : ApiController
{
    private readonly IMapper _mapper;
    private readonly ITaskService _taskService;
    private readonly IUserIdentityService _userIdentityService;

    public TaskController(IMapper mapper, ITaskService userService, IUserIdentityService userIdentityService)
    {
        _mapper = mapper;
        _taskService = userService;
        _userIdentityService = userIdentityService;
    }

    [Route("create")]
    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] CreateTask input)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);
        
        var userId = _userIdentityService.GetUserId();
        
        var task = _mapper.Map<Task>(input);
        task.UserId = userId;
        var result = await _taskService.Create(task);
        
        return result.Match(
            res => Success(StatusCodes.Status200OK, _mapper.Map<DetailTask>(res)),
            err => Problem(err));
    }

    [Route("update")]
    [HttpPut]
    public async Task<IActionResult> UpdateTask([FromBody] UpdateTask input)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var task = _mapper.Map<Task>(input);
        var result = await _taskService.Update(task);
        return result.Match(
            res => Success(StatusCodes.Status200OK, _mapper.Map<DetailTask>(res)),
            err => Problem(err));

    }

    [Route("get/detail/{id}")]
    [HttpGet]
    public async Task<IActionResult> GetTaskDetail(Guid id)
    {
        var result = await _taskService.GetById(id);
        return result.Match(
            res => Success(StatusCodes.Status200OK, _mapper.Map<DetailTask>(res)),
            err => Problem(err));
    }

    [Route("get/user/id")]
    [HttpGet]
    public async Task<IActionResult> GetTaskByUser([FromQuery] TaskFilter input)
    {
        var userId = _userIdentityService.GetUserId();

        var tasks = _taskService.GetByUserId(input.CreateFrom, input.CreateTo,
            input.DateFrom, input.DateTo, input.PriorityFrom, input.PriorityTo,
            input.IsFinish, userId, input.OrderByDueDate, input.OrderByPriority,
            input.Limit, input.Page);

        var metaData = new MetaData()
        {
            TotalCount = tasks.Result.Value.TotalCount,
            PageSize = tasks.Result.Value.Limit,
            CurrentPage = tasks.Result.Value.CurrentPage,
            TotalPages = tasks.Result.Value.TotalPages,
            HasNextPage = tasks.Result.Value.HasNextPage,
            HasPreviousPage = tasks.Result.Value.HasPreviousPage
        };
        
        return tasks.Result.Match(
            res => Success(
                StatusCodes.Status200OK,
                res.Data.Select(p => _mapper.Map<ResponseTask>(p)),
                "list payment",
                metaData),
            err => Problem(err));
    }
}