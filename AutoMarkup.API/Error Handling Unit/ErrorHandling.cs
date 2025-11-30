using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;

namespace Construction.API.Error_Handling_Unit
{
    public static class ErrorHandling
    {
        public static async Task<dynamic> TryCatch(
            Func<Task<dynamic>> func,
            ILogger logger,
            string? customErrorMessage = null)
        {
            try
            {
                var result = await func();

                if (result is ActionResult actionResult)
                    return actionResult;

                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in TryCatch");
                return CreateErrorResult(ex, customErrorMessage);
            }
        }

        private static ActionResult CreateErrorResult(Exception ex, string? customErrorMessage = null)
        {
            var (statusCode, errorType) = GetStatusCodeAndType(ex);
            var errorMessage = customErrorMessage ?? GetUserFriendlyMessage(ex);

            var problemDetails = new ProblemDetails
            {
                Title = errorType,
                Detail = errorMessage,
                Status = statusCode,
                Type = GetErrorTypeUri(statusCode),
                Instance = null
            };

            problemDetails.Extensions["timestamp"] = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
            problemDetails.Extensions["errorCode"] = GetErrorCode(ex);

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                problemDetails.Extensions["stackTrace"] = ex.StackTrace;
                problemDetails.Extensions["innerException"] = ex.InnerException?.Message;
            }

            return new ObjectResult(problemDetails)
            {
                StatusCode = statusCode
            };
        }

        private static (int statusCode, string errorType) GetStatusCodeAndType(Exception ex)
        {
            return ex switch
            {
                ArgumentException or ArgumentNullException or ArgumentOutOfRangeException
                    => (400, "BAD_REQUEST"),

                UnauthorizedAccessException
                    => (401, "UNAUTHORIZED"),

                System.Security.SecurityException
                    => (403, "FORBIDDEN"),

                KeyNotFoundException or FileNotFoundException or DirectoryNotFoundException
                    
                    => (404, "NOT_FOUND"),

                TimeoutException or System.Threading.Tasks.TaskCanceledException
                    => (408, "REQUEST_TIMEOUT"),

                InvalidOperationException or System.IO.IOException
                    => (409, "CONFLICT"),

                ValidationException
                    => (422, "VALIDATION_ERROR"),

                HttpRequestException httpEx when httpEx.Message.Contains("Too Many Requests")
                    => (429, "TOO_MANY_REQUESTS"),

                SqlException or DbException
                    => (503, "SERVICE_UNAVAILABLE"),

                NotImplementedException
                    => (501, "NOT_IMPLEMENTED"),

                HttpRequestException httpEx when httpEx.StatusCode.HasValue
                    => ((int)httpEx.StatusCode.Value, "HTTP_ERROR"),

                _ => (500, "INTERNAL_SERVER_ERROR")
            };
        }

        private static string GetErrorTypeUri(int statusCode)
        {
            return $"https://httpstatuses.com/{statusCode}";
        }

        private static string GetErrorCode(Exception ex)
        {
            return ex.GetType().Name.ToUpper().Replace("EXCEPTION", "_ERROR");
        }

        private static string GetUserFriendlyMessage(Exception ex)
        {
            return ex switch
            {
                SqlException sqlEx when sqlEx.Number == 2627 || sqlEx.Number == 2601
                    => "This record already exists in the system.",

                SqlException sqlEx when sqlEx.Number == 547
                    => "This operation cannot be completed because related records exist.",

                SqlException sqlEx when sqlEx.Number == 1205
                    => "The operation could not be completed due to a system conflict. Please try again.",

                TimeoutException
                    => "The request timed out. Please try again.",

                UnauthorizedAccessException
                    => "You are not authorized to perform this action.",

                KeyNotFoundException
                    => "The requested resource was not found.",

                _ => ex.Message
            };
        }
    }
}