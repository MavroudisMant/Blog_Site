namespace Blog_Site.Interfaces
{
    public interface ICommentRepository
    {
        public Task<List<Comment>> GetAllComments(int id);
        public Task<List<Comment>> GetCommentsByUserAsync(int user_id);
        public Task<List<Comment>> GetCommentsByBlogAsync(int blog_id);
        public void DeleteCommentAsync(int com_id);
        public Task<Comment> UpdateCommentsAsync (Comment comment);
        public Task<Comment> GetCommentByIdAsync(int id);
        public Task<Comment> CreateCommentAsync(Comment comment);

    }
}
