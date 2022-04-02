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

        public IActionResult Index(int? pageNo, int? nextPage)
        {

            var blogs = _blogManager.GetAll();
           

           

            double pageCount = Convert.ToDouble(blogs.Count) / 2;
            ViewBag.PageSize = Math.Round(pageCount);
            ViewBag.PageNext = pageNo+1;

            HomeVM vm = new()
            {
                Blogs = _blogManager.GetAll(pageNo)
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