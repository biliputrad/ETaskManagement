using ETaskManagement.Api.Common.Constant;
using ETaskManagement.Contract.Common.Response;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ETaskManagement.Api.Controllers;

[ApiController]
public class ApiController : ControllerBase
{
    protected IActionResult Problem(List<Error> errors)
    {
        if (errors.All(error => error.Type == ErrorType.Validation))
        {
            return ValidationProblem(errors);
        }

        HttpContext.Items[HttpContextItemKeys.Errors] = errors;

        var firstError = errors[0];

        return Problem(firstError);
    }

    private IActionResult Problem(Error firstError)
    {
        var statusCode = firstError.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };
        return Problem(statusCode: statusCode, title: firstError.Description);
    }
    
    private IActionResult ValidationProblem(List<Error> errors)
    {
        var modelStateDictionary = new ModelStateDictionary();

        foreach (Error error in errors)
        {
            modelStateDictionary.AddModelError(
                error.Code,
                error.Description);
        }

        return ValidationProblem(modelStateDictionary);
    }
    
    protected IActionResult Success(int status, object data, string? message = null, MetaData? metadata = null)
    { 
        message ??= "success";
        
        var response = new ResponseSuccess(
            status,
            message,
            data, 
            metadata
        );

        switch (status)
        {
            case StatusCodes.Status201Created:
                return Created("",response);
            case StatusCodes.Status202Accepted:
                return Accepted(response);
            case StatusCodes.Status204NoContent:
                return NoContent();
            default:
                return Ok(response);
        }
    }
}