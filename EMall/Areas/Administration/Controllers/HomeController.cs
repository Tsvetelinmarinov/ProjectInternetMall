using EMall.Areas.Administration.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EMall.Areas.Administration.Controllers
{
    /// <summary>
    ///  Provides endpoint for the administrator page.
    /// </summary>
    [Area("Administration")]
    [Authorize(Roles = "Administrator")]
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(AdminDashboardViewModel.Demo());
        }
    }
}