using EMall.PipelineFilters.ResultFilters.Options;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace EMall.PipelineFilters.ResultFilters
{
    /// <summary>
    ///  Result filter that adds the header X-Developer.
    ///  The 'X-Developer' header will hold different value when we are
    ///  in production and when development.
    /// </summary>
    public class AsyncResultFilterForAddingXDeveloperHeader(IOptions<XDeveloperHeaderOptions> xDevHeaderOptions) 
        : IAsyncResultFilter
    {
        // The name of the developer.
        private readonly string _developerName 
            = xDevHeaderOptions.Value.DeveloperFullName; 

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            // Adding header X-Developer in the response headers before the
            // response starts returning.
            context
                .HttpContext
                .Response
                .Headers
                .Append("X-Developer", new(this._developerName));

            _ = await next.Invoke(); // Next, actually is the action in the controller,
                                    // beacuse there is no more filters, and we reach the endpoint.
                                   // After the IActionResult is rendered, the result goes out as
                                  // clear html file.

            // No filter after the view rendering.
        }
    }
}