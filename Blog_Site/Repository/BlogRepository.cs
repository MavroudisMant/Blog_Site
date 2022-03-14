using Blog_Site.Data;
using Blog_Site.Interfaces;

namespace Blog_Site.Repository
{
    public class BlogRepository : IBlogRepository
    {
        private readonly DataContext _context;

        public BlogRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Blog> CreateBlogAsync(Blog blog)
        {
            _context.Add(blog);
            await _context.SaveChangesAsync();
            return blog;
        }

        public async Task<List<Blog>> GetAllBlogsAsync()
        {
            return await _context.Blogs.ToListAsync();
        }

        public async Task<Blog> GetBlogByIdAsync(int id)
        {
            var blog = await _context.Blogs.FirstOrDefaultAsync(b => b.Id == id);
            if (blog == null)
            {
                return null;
            }

            return blog;
        }

        public async Task<List<Blog>> GetBlogsByUserIdAsync(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
