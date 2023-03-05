using Assignment.Contexts;
using Assignment.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _dataContext;

        public HomeController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IActionResult> Products()
        {
            var viewModel = new ProductsViewModel();
            var appProducts = await _dataContext.Products.ToListAsync();

            foreach (var prod in appProducts)
            {
                viewModel.Products.Add(prod);
            }
            return View(viewModel);
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new ProductsViewModel();
            var appProducts = await _dataContext.Products.ToListAsync();

            foreach (var prod in appProducts)
            {
                viewModel.Products.Add(prod);
            }
            return View(viewModel);
        }
    }
}
