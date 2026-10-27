using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EMall.Controllers
{
    /// <summary>
    ///  Provides endpoints for the Home page and Error page.
    /// </summary>
    public class HomeController : CoreController
    {
        /// <summary>
        ///  Loads Home Page.
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///   Loads the Error Page.
        /// </summary>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [AllowAnonymous]
        public IActionResult Error()
        {
            return View();
        }
    }
}