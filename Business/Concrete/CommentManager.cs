using Business.Abstract;
using DataAccess;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CommentManager : ICommentManager
    {
        private readonly BlogDbContext _context;

        public CommentManager(BlogDbContext context)
        {
            _context = context;
        }

        public void AddComment(Comment comment)
        {
            comment.PublishDate = DateTime.Now;
            _context.Add(comment);
            _context.SaveChanges();
        }

        public List<Comment> GetBlogComment(int blogId)
        {
            var blogComment = _context.Comments.Where(x => x.BlogID == blogId).ToList();
            return blogComment;
        }
    }
}
