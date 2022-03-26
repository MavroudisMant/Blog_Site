namespace Blog_Site.Interfaces
{
    public interface IBlogRepository
    {
        public Task<List<BlogInfo>> GetAllBlogsAsync();
        public Task<Blog> GetBlogByIdAsync(int id);
        public Task<List<Blog>> GetBlogsByUserIdAsync(int userId);
        public Task<Blog> CreateBlogAsync(Blog blog);
        public Task<Blog> UpdateBlogAsync(Blog blog);
        void DeleteBlog(int id);
        public Task<Blog> RateBlogAsync(int id);
    }
}
