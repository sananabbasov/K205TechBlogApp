using Business.Abstract;
using DataAccess;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class BlogManager : IBlogManager
    {
        private readonly BlogDbContext _context;
        

        public BlogManager(BlogDbContext context)
        {
            _context = context;
        }

        public void Create(Blog blog)
        {
            blog.PublishDate = DateTime.Now;
            blog.UpdateDate = DateTime.Now;
            _context.Blogs.Add(blog);
            _context.SaveChanges();
        }

        public List<Blog> GetAll(int? pageNo, int recordSize)
        {
            if (pageNo == null)
            {
                pageNo = 1;
            }



            int currentPage = 2 * pageNo.Value - 2;


            var blogs = _context.Blogs.Skip(currentPage).Take(recordSize).Include(x => x.Category).Include(x => x.K205User).ToList();
            return blogs;
        }

        public List<Blog> GetAll()
        {
            var blogs = _context.Blogs.ToList();
            return blogs;
        }


        public int GetAllCount()
        {
            var blogs = _context.Blogs.ToList();
            return blogs.Count();
        }

        public Blog GetById(int? id) // 4
        {
            
            var blog = _context.Blogs.Include(x=>x.Category).Include(x=>x.K205User).FirstOrDefault(x => x.ID == id.Value);

            blog.Hit += 1;  // 133 + 1, 134 + 1



            Update(blog);

            return blog;
        }

        public List<Blog> Similar(int catId, string userId, int blogId)
        {
            var similarBlog = _context.Blogs.OrderByDescending(x => x.Hit).Include(x=>x.Category).Include(x=>x.K205User)
                .Where(x => x.CategoryID == catId && x.K205UserId == userId && x.ID != blogId).Take(2).ToList();

            return similarBlog;
        }

        public void Update(Blog blog)
        {
            _context.Blogs.Update(blog);
            _context.SaveChanges();
        }
    }
}
