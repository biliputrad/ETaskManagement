using ETaskManagement.Contract.Login.Request;
using ETaskManagement.Contract.Task.Request;
using FluentValidation;

namespace ETaskManagement.Api.Common.Validator;

public class CreateTaskValidator : AbstractValidator<CreateTask>
{
    public CreateTaskValidator()
    {
        RuleFor(x => x.TaskName).NotEmpty().NotNull();
        RuleFor(x => x.DueDate).NotEmpty().NotNull();
        RuleFor(x => x.PriorityLevel).NotEmpty().NotNull();
        RuleFor(x => x.IsFinished).NotNull();
    }
}

public class UpdateTaskValidator : AbstractValidator<UpdateTask>
{
    public UpdateTaskValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
        RuleFor(x => x.IsFinished).NotNull();
    }
}
