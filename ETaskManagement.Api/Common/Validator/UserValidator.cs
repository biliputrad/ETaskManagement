using ETaskManagement.Contract.Login.Request;
using ETaskManagement.Contract.User.Request;
using FluentValidation;

namespace ETaskManagement.Api.Common.Validator;

public class CreateUserValidator : AbstractValidator<CreateUser>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.FirstName).NotEmpty().NotNull();
        RuleFor(x => x.Password).NotEmpty().NotNull();
        RuleFor(x => x.Division).NotEmpty().NotNull();
        RuleFor(x => x.Roles).NotEmpty().NotNull();
    }
}

public class UpdateUserValidator : AbstractValidator<UpdateUser>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().NotNull();
        RuleFor(x => x.Password).NotEmpty().NotNull();
        RuleFor(x => x.Division).NotEmpty().NotNull();
        RuleFor(x => x.Roles).NotEmpty().NotNull();
    }
}

public class LoginUserValidator : AbstractValidator<LoginRequest>
{
    public LoginUserValidator()
    {
        RuleFor(x => x.Email).EmailAddress().NotEmpty().NotNull();
        RuleFor(x => x.Password).NotEmpty().NotNull();
    }
}