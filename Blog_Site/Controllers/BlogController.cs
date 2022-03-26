using Blog_Site.Data;
using Blog_Site.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog_Site.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogRepository _blogRepository;

        public BlogController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<BlogInfo>>> GetAll()
        {
            return Ok(await _blogRepository.GetAllBlogsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Blog>> GetById(int id)
        {
            string token = Request.Headers["Authorization"];
            var blog = await _blogRepository.GetBlogByIdAsync(id);
            if (blog == null)
            {
                return NotFound();
            }

            return Ok(blog);
        }

        [HttpGet("user_blogs/{userId}")]
        public async Task<ActionResult<List<Blog>>> GetByUserId(int userId)
        {
            var blogs = await _blogRepository.GetBlogsByUserIdAsync(userId);
            if(blogs == null)
            {
                return BadRequest("The user has not written any blogs");
            }

            return Ok(blogs);
        }

        [HttpPost]
        [Authorize(Roles = "writer")]
        public async Task<ActionResult<Blog>> CreateBlog(Blog blog)
        {
            blog = await _blogRepository.CreateBlogAsync(blog);
            return Ok(blog);
        }

        //edit the title and body of the blog
        [HttpPut]
        public async Task<ActionResult<List<Blog>>> UpdateBlog(Blog blog)
        {
            var dbblog = await _blogRepository.GetBlogByIdAsync(blog.Id);
            if (dbblog == null)
                return BadRequest("blog doesn't exist");
            else
                await _blogRepository.UpdateBlogAsync(blog);
            

            return Ok(await _blogRepository.GetAllBlogsAsync());
        }

        //delete a blog
        [HttpDelete("blog")]
        public async Task<ActionResult<List<Blog>>> DeleteBlog(int blogId)
        {
            var blog = await _blogRepository.GetBlogByIdAsync(blogId);
            if (blog == null)
                return BadRequest("Blog doesn't exist");
            else
                _blogRepository.DeleteBlog(blog.Id);

            return Ok(await _blogRepository.GetAllBlogsAsync());


        }

        //add rating to the blog
        [HttpPut("rating")]
        public async  Task<ActionResult<Blog>> RateBlog(int id)
        {
            var dblog = await _blogRepository.GetBlogByIdAsync(id);
            if (dblog == null)
                return BadRequest("Blog doesn't exist");
            else
                await _blogRepository.RateBlogAsync(dblog.Id);

            return Ok(await _blogRepository.GetBlogByIdAsync(id));
        }
    }
}
