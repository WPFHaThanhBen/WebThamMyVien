using APICosmeticClinic.Dto;
using APICosmeticClinic.Interfaces;
using APICosmeticClinic.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APICosmeticClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public PostController(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        // Get ALL 
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Post>))]
        public IActionResult GetAllPost()
        {
            var posts = _mapper.Map<List<PostDto>>(_postRepository.GetAllPost());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(posts);
        }

        // Get By ID
        [HttpGet("{postIdRepository}")]
        [ProducesResponseType(200, Type = typeof(Post))]
        //[ProducesResponseType(400)]
        public IActionResult GetPost(int postIdRepository)
        {
            if (!_postRepository.PostExists(postIdRepository))
                return NotFound();

            var map = _mapper.Map<PostDto>(_postRepository.GetPost(postIdRepository));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(map);
        }

        //Create
        [HttpPost]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        public IActionResult CreatePost([FromBody] PostDto create)
        {
            // id tự tăng nên không cần check Exitsts (suy nghỉ cá nhân thoi).
            if (create == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var map = _mapper.Map<Post>(create);

            if (!_postRepository.CreatePost(map))
            {
                ModelState.AddModelError("", "Something went wrong while create");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        // Update
        [HttpPut("{postIdRepository}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult UpdateAction(int postIdRepository, [FromBody] PostDto updated)
        {
            if (updated == null)
                return BadRequest(ModelState);

            if (postIdRepository != updated.Id)
                return BadRequest(ModelState);

            if (!_postRepository.PostExists(postIdRepository))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var map = _mapper.Map<Post>(updated);

            if (!_postRepository.UpdatePost(map))
            {
                ModelState.AddModelError("", "Something went wrong updating Post");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Updated");
        }

        // Delete
        [HttpDelete("{postIdRepository}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult DeletePost(int postIdRepository)
        {
            if (!_postRepository.PostExists(postIdRepository))
            {
                return NotFound();
            }

            var delete = _postRepository.GetPost(postIdRepository);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_postRepository.DeletePost(delete))
            {
                ModelState.AddModelError("", "Something went wrong deleting Post");
            }

            return Ok("Successfully Deleted");
        }
    }
}
