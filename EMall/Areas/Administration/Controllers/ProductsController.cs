using EMall.Core.Services.Interfaces;
using EMall.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EMall.Areas.Administration.Controllers
{
    public class ProductsController(ICategoryService service) : AdminCoreController
    {
        [HttpGet]
        public async Task<IActionResult> AddProduct()
        {
            var categoryServiceModels = await service.GetAllCategoriesAsync();
            
            IEnumerable<SelectListItem> categorySelectItems = categoryServiceModels
                    .Select((category) => new SelectListItem 
                    {
                        Text = category.CategoryName,
                        Value = category.CategoryName
                    });

            ViewBag.Categories = categorySelectItems;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromForm] ProductViewModel productModel)
        {
            if (this.ModelState.IsValid is false)
            {
                return View(productModel);
            }

            return Ok();
        }
    }
}