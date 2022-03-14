namespace Blog_Site.Interfaces
{
    public interface IBlogRepository
    {
        public Task<List<Blog>> GetAllBlogsAsync();
        public Task<Blog> GetBlogByIdAsync(int id);
        public Task<List<Blog>> GetBlogsByUserIdAsync(int userId);
        public Task<Blog> CreateBlogAsync(Blog blog);
    }
}
