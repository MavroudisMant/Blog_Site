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
        public async Task<List<BlogInfo>> GetAllBlogsAsync()
        {
            return await _context.Blogs.Select(b => new BlogInfo(){
                BlogId = b.Id,
                BlogTitle = b.Title,
                AuthorId = b.Author.Id,
                AuthorName = b.Author.UserName
            }).ToListAsync();
        }

        public async Task<Blog> GetBlogByIdAsync(int id)
        {
            var blog = await _context.Blogs.Include(b => b.Comments).FirstOrDefaultAsync(b => b.Id == id);

            return blog;
        }

        public async Task<List<Blog>> GetBlogsByUserIdAsync(int userId)
        {
            var blogs = await _context.Blogs.Where(b => b.Author.Id == userId).ToListAsync();
            return blogs;
        }

        public async void DeleteBlog(int id)
        {
            var blog = await GetBlogByIdAsync(id);
            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();

        }

        public async Task<Blog> UpdateBlogAsync(Blog blog)
        {
            var dblog = await GetBlogByIdAsync(blog.Id);
            dblog.Body = blog.Body;
            dblog.Title = blog.Title;

            await _context.SaveChangesAsync();

            return dblog;
        }

        public async Task<Blog> RateBlogAsync(int id)
        {
            var dblog = await _context.Blogs.Where(b => b.Id == id).FirstOrDefaultAsync();
            dblog.Upvotes = dblog.Upvotes + 1;

            await _context.SaveChangesAsync();
            return dblog;
        }
    }
}
