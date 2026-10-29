using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EMall.Controllers
{
    /// <summary>
    ///  Provides endpoints for Catalog page.
    /// </summary>
    [AllowAnonymous]
    public class CatalogController : CoreController
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}