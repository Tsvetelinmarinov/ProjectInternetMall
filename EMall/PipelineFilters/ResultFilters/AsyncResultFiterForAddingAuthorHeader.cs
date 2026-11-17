using Microsoft.AspNetCore.Mvc.Filters;

namespace EMall.PipelineFilters.ResultFilters
{
    /// <summary>
    ///  Result filter that adds the header X-Developer.
    ///  The 'X-Developer' header will hold different value when we are
    ///  in production and when development.
    /// </summary>
    public class AsyncResultFilterForAddingXDeveloperHeader : IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            // Adding header X-Developer in the response headers before the
            // response starts returning.
            bool successHeaderAppending = context
                .HttpContext
                .Response
                .Headers
                .TryAdd("X-Developer", new("Tsvetelin Marinov"));

            // Render if everything's fine.
            if (successHeaderAppending is true)
            {
                _ = await next.Invoke(); // Next, actually is the action in the controller,
                                        // beacuse there is no more filters, and we reach the endpoint.
                                       // After the IActionResult is rendered, the result goes out as
                                      // clear html file.
            }

            // No filter after the view rendering.
        }
    }
}