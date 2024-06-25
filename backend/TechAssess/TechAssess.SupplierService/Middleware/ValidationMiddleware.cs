using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json;
using TechAssess.SupplierService.Dto;
using TechAssess.SupplierService.Enums;

namespace TechAssess.SupplierService.Exceptions
{
    public class ValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public ValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;
            try
            {
                await _next(context);
                if (context.Response.StatusCode == StatusCodes.Status400BadRequest )
                {
                    responseBody.Seek(0, SeekOrigin.Begin);
                    var responseContent = await new StreamReader(responseBody).ReadToEndAsync();
                    var validationProblemDetails = JsonSerializer.Deserialize<ValidationProblemDetails>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    if (validationProblemDetails != null && validationProblemDetails.Status != null)
                    {
                        var errorMessage = ConcatenateErrors(validationProblemDetails);
                        var customResponse = new ApiResponse<object>
                        {
                            Data = null,
                            ErrorCode = ErrorCode.ValidationError.ToString(),
                            ErrorMessage = errorMessage
                        };

                        var customResponseJson = JsonSerializer.Serialize(customResponse);
                        context.Response.Body = originalBodyStream;
                        await context.Response.WriteAsync(customResponseJson);
                    }
                    else
                    {
                        responseBody.Seek(0, SeekOrigin.Begin);
                        await responseBody.CopyToAsync(originalBodyStream);
                    }
                }
                else
                {
                    responseBody.Seek(0, SeekOrigin.Begin);
                    await responseBody.CopyToAsync(originalBodyStream);
                }
            }
            catch (Exception ex)
            {
                context.Response.Body = originalBodyStream;
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                var customErrorResponse = new ApiResponse<object>
                {
                    Data = null,
                    ErrorCode = ErrorCode.InternalServerError.ToString(),
                    ErrorMessage = ex.Message
                };
                var customErrorResponseJson = JsonSerializer.Serialize(customErrorResponse);
                await context.Response.WriteAsync(customErrorResponseJson);
            }
            finally
            {
                context.Response.Body = originalBodyStream;
            }
        }


        private static string ConcatenateErrors(ValidationProblemDetails validationProblemDetails)
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine(validationProblemDetails.Title);

            foreach (var error in validationProblemDetails.Errors)
            {
                foreach (var errorMessage in error.Value)
                {
                    sb.AppendLine(errorMessage);
                }
            }

            return sb.ToString().TrimEnd();
        }
    }
}
