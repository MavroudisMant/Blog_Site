using Blog_Site.Data;
using Blog_Site.Interfaces;

namespace Blog_Site.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DataContext _context;

        public CommentRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Comment>> GetCommentsByUserAsync(int user_id)
        {
            return await _context.Comments.Where(c => c.User_Id == user_id).ToListAsync();
        }

        public async void DeleteCommentAsync(int com_id)
        {
            var comment = await _context.Comments.FindAsync(com_id);
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

        }

        public async Task<Comment> UpdateCommentsAsync(Comment comment)
        {
            var dbcomment = await _context.Comments.FindAsync(comment.Id);
            dbcomment.Body = comment.Body;

            await _context.SaveChangesAsync();

            return dbcomment;
        }

        public async Task<List<Comment>> GetAllComments(int id)
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<List<Comment>> GetCommentsByBlogAsync(int blog_id)
        {
            return await _context.Comments.Where(c => c.Blog_Id == blog_id).ToListAsync();

        }

        public async Task<Comment> GetCommentByIdAsync(int id)
        {
            return  await _context.Comments.FindAsync(id); ;
        }

        public async Task<Comment> CreateCommentAsync(Comment comment)
        {
            _context.Add(comment);
            await _context.SaveChangesAsync();
            return comment;
        }
    }
}
