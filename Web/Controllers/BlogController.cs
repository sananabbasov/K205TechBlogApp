using Business.Abstract;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModel;

namespace Web.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogManager _blogManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<K205User> _userManager;
        private readonly ICommentManager _commentManager;
        public BlogController(IBlogManager blogManager, UserManager<K205User> userManager, ICommentManager commentManager)
        {
            _blogManager = blogManager;
            _userManager = userManager;
            _commentManager = commentManager;
        }

        public IActionResult Detail(int? id)
        {
            var blog = _blogManager.GetById(id);

            DetailVM detailVM = new()
            {
                Blog = blog,
                User = _userManager.FindByIdAsync(blog.K205UserId).Result,
                Similar = _blogManager.Similar(blog.CategoryID, blog.K205UserId, blog.ID),
                Comments = _commentManager.GetBlogComment(blog.ID)
            };
            return View(detailVM);
        }
    }
}
