using Core.Helper;
using Entities;

namespace Web.ViewModel
{
    public class HomeVM
    {
        public List<Blog> Blogs { get; set; }
        public Pager Pager { get; set; }
    }
}
