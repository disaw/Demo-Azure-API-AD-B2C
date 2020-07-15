using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using WebApp.Services;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{    
    public class ManageProductsController : Controller
    {
        private readonly IProductService _productService;

        public ManageProductsController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _productService.Read());
        }

        [Authorize]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productService.Read(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,Model,Brand")] Product product)
        {
            if (ModelState.IsValid)
            {
                await _productService.Create(product);

                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        [Authorize]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productService.Read(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Description,Model,Brand")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _productService.Update(product);

                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productService.Read(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var product = await _productService.Read(id);

            await _productService.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
