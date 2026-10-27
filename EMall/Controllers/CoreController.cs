using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EMall.Controllers
{
    /// <summary>
    ///  Describes core controller functionality for all other Controllers.
    /// </summary>
    [Authorize]
    public class CoreController : Controller
    {
        /// <summary>
        ///  Gets the value that indicates if the current user in logged in.
        /// </summary>
        public bool? IsUserAuthenticated =>
            this.User?.Identity?.IsAuthenticated ?? false;
    }
}