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
        private readonly ICategoryManager _categoryManager;
        public BlogController(IBlogManager blogManager, UserManager<K205User> userManager, ICommentManager commentManager, ICategoryManager categoryManager)
        {
            _blogManager = blogManager;
            _userManager = userManager;
            _commentManager = commentManager;
            _categoryManager = categoryManager;
        }

        public IActionResult Detail(int? id)
        {
            var blog = _blogManager.GetById(id);

            var comments = _commentManager.GetBlogComment(blog.ID);

            ViewBag.Comments = comments.Count;

            DetailVM detailVM = new()
            {
                Blog = blog,
                User = _userManager.FindByIdAsync(blog.K205UserId).Result,
                Similar = _blogManager.Similar(blog.CategoryID, blog.K205UserId, blog.ID),
                Comments = comments,
                Categories = _categoryManager.GetAll()
            };
            return View(detailVM);
        }


        [HttpPost]
        public IActionResult AddBlogComment(Comment comment, int blogId)
        {
            comment.BlogID = blogId;
            _commentManager.AddComment(comment);
            return RedirectToAction("Detail", "Blog", new { id = blogId });
        }
    }
}
