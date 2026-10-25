using Microsoft.AspNetCore.Mvc;

namespace EMall.Controllers
{
    /// <summary>
    ///  Provides endpoints for the Home page and Error page.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        ///  Loads Home Page.
        /// </summary>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///   Loads the Error Page.
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}