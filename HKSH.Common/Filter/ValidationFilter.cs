using HKSH.Common.Context;
using HKSH.Common.ShareModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace HKSH.Common.Filter
{
    /// <summary>
    /// ValidationFilter
    /// </summary>
    /// <seealso cref="IActionFilter" />
    internal class ValidationFilter : IActionFilter
    {
        /// <summary>
        /// Called before the action executes, after model binding is complete.
        /// </summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext" />.</param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            bool isAllowAnonymous = context.Controller is not ControllerBase controller
                || controller.ControllerContext.ActionDescriptor.ControllerTypeInfo.IsDefined(typeof(IAllowAnonymous), true)
                || controller.ControllerContext.ActionDescriptor.MethodInfo.IsDefined(typeof(IAllowAnonymous), true);

            // Check blacklist
            if (!isAllowAnonymous)
            {
                ICurrentContext? currentContext = context.HttpContext.RequestServices.GetService<ICurrentContext>();
                if (currentContext == null || string.IsNullOrEmpty(currentContext.CurrentUser.UserId))
                {
                    context.Result = new UnauthorizedObjectResult(MessageResult.FailureResult());
                    return;
                }
            }
        }

        /// <summary>
        /// Called after the action executes, before the action result.
        /// </summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ActionExecutedContext" />.</param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}