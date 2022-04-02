using Entities;

namespace Web.ViewModel
{
    public class DetailVM
    {
        public Blog Blog { get; set; }
        public List<Blog> Blogs { get; set; }
        public K205User User { get; set; }
        public List<Blog> Similar { get; set; }
        public List<Comment> Comments { get; set; }
        public Comment Comment { get; set; }
        public List<Category> Categories { get; set; }
    }
}
