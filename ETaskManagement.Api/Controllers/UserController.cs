using AutoMapper;
using ETaskManagement.Application.Token;
using ETaskManagement.Application.User;
using ETaskManagement.Application.UserIdentity;
using ETaskManagement.Contract.Login.Request;
using ETaskManagement.Contract.Login.Response;
using ETaskManagement.Contract.User.Request;
using ETaskManagement.Contract.User.Response;
using ETaskManagement.Domain.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETaskManagement.Api.Controllers;

[Route("user")]
public class UserController : ApiController
{
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;
    private readonly IUserIdentityService _userIdentityService;

    public UserController(IMapper mapper, IUserService userService, ITokenService tokenService, IUserIdentityService userIdentityService)
    {
        _mapper = mapper;
        _userService = userService;
        _tokenService = tokenService;
        _userIdentityService = userIdentityService;

    }

    [Route("create")]
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUser input)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var user = _mapper.Map<User>(input);
        var result = await _userService.Create(user);
        
        return result.Match(
            res => Success(StatusCodes.Status200OK, _mapper.Map<UserResponse>(res)),
            err => Problem(err));
    }
    
    [Route("userprofile")]
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUserProfile()
    {
        var userId = _userIdentityService.GetUserId();
        var result = await _userService.GetById(userId);
        
        return result.Match(
            res => Success(StatusCodes.Status200OK, _mapper.Map<UserResponse>(res)),
            err => Problem(err));
    }
    
    [Route("update")]
    [HttpPut]
    public async Task<IActionResult> UpdateUser([FromBody]UpdateUser request)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);
        
        var payment = _mapper.Map<User>(request);
        var result = await _userService.Update(payment);

        return result.Match(
            res => Success(StatusCodes.Status200OK, _mapper.Map<UserResponse>(res)),
            err => Problem(err));
    }

    [Route("login")]
    [HttpPost]
    public async Task<IActionResult> Login([FromBody]LoginRequest request)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var input = _mapper.Map<User>(request);
        var result = await _tokenService.GenerateTokenUser(input);
        return result.Match(
            res => Success(StatusCodes.Status200OK, _mapper.Map<LoginResponse>(res)),
            err => Problem(err));
    }


}