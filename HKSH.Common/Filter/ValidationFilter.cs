using HKSH.Common.Context;
using HKSH.Common.ShareModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace HKSH.Common.Filter
{
    internal class ValidationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.Controller as ControllerBase;
            bool isAllowAnonymous = controller == null
                || controller.ControllerContext.ActionDescriptor.ControllerTypeInfo.IsDefined(typeof(IAllowAnonymous), true)
                || controller.ControllerContext.ActionDescriptor.MethodInfo.IsDefined(typeof(IAllowAnonymous), true);

            // Check blacklist
            if (!isAllowAnonymous)
            {
                ICurrentContext? currentContext = context.HttpContext.RequestServices.GetService<ICurrentContext>();
                if (currentContext == null || currentContext.CurrentUser.Id == 0)
                {
                    context.Result = new UnauthorizedObjectResult(MessageResult.FailureResult());
                    return;
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}