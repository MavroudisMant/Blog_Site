using Blog_Site.Data;
using Blog_Site.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog_Site.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;

        public CommentController (ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        //get all user's comments
        [HttpGet("{user_id}")]
        public async Task<ActionResult<List<Comment>>> GetBlogByUser(int id)
        {
            return Ok(await _commentRepository.GetCommentsByUserAsync(id));
        }

        //delete a comment based on the id
        [HttpDelete]
        public async Task<ActionResult<List<Comment>>> DeleteComment(int id)
        {
            var comment = await _commentRepository.GetCommentsByUserAsync(id);
            return Ok("The comment has been deleted");
        }

        //edit a comment
        [HttpPut]
        public async Task<ActionResult<Comment>> UpdateComment(Comment comment)
        {
            var dbcom = await _commentRepository.GetCommentByIdAsync(comment.Id);
            if (dbcom == null)
                return BadRequest("Commnent not found");
            else
                await _commentRepository.UpdateCommentsAsync(dbcom);
            return Ok(dbcom);
        }

        //create a comment
        [HttpPost]
        public async Task<ActionResult> CreateCommentAsync(Comment comment)
        {
            await _commentRepository.CreateCommentAsync(comment);

            return Ok(comment);
        }

    }
}
