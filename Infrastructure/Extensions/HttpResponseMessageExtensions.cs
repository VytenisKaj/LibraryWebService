using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Infrastructure.Extensions
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task<string> GetErrorsFromHttpResponse(this HttpResponseMessage response)
        {
            var contents = JsonSerializer.Deserialize<ProblemDetails>(await response.Content.ReadAsStringAsync());
            return contents.Extensions["errors"].ToString();
        }

        public static int CreatedObjectId(this HttpResponseMessage response)
        {
            return Int32.Parse(response.Headers.Location.AbsolutePath.Split('/').Last());
        }
    }
}
