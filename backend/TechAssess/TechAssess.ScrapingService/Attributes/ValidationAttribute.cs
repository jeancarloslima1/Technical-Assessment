using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.RegularExpressions;

namespace TechAssess.ScrapingService.Attributes
{
    public class ValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
            {
                var supplierName = (string)context.ActionArguments["supplierName"]!;

                if (Regex.IsMatch(supplierName, @"^[a-zA-Z0-9\s&\-_\.!,]+$"))
                {
                    return;
                }
            }

            context.Result = new BadRequestObjectResult(new
            {
                ErrorMessage = "Only alphanumeric characters are allowed for supplierName"
            });
        }
    }
}
