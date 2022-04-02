using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web.Models;
using Web.ViewModel;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogManager _blogManager;
        public HomeController(ILogger<HomeController> logger, IBlogManager blogManager)
        {
            _logger = logger;
            _blogManager = blogManager;
        }

        public IActionResult Index(int? pageNo)
        {

            var blogs = _blogManager.GetAll(pageNo);


            HomeVM vm = new()
            {
               Blogs = blogs
            };
            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}