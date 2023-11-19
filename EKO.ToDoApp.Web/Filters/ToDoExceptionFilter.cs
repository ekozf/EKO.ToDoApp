using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using EKO.ToDoApp.Shared.Exceptions;

namespace EKO.ToDoApp.Web.Filters;

/// <summary>
/// Filters the exceptions thrown by the controllers and returns the appropriate HTTP status code.
/// </summary>
public sealed class ToDoExceptionFilter : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        if (context.Exception is UserNotFoundException || context.Exception is ItemNotFoundException)
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            context.Result = new NotFoundResult();
        }
        else if (context.Exception is InvalidPasswordException)
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Result = new UnauthorizedResult();
        }
        else if (context.Exception is UserAlreadyExistsException)
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status409Conflict;
            context.Result = new ConflictResult();
        }
        else
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Result = new ObjectResult(context.Exception.Message);
        }
    }
}

