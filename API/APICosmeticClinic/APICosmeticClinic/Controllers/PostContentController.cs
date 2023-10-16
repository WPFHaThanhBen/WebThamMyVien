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
    public class PostContentController : ControllerBase
    {
        private readonly IPostContentRepository _postContentRepository;
        private readonly IMapper _mapper;

        public PostContentController(IPostContentRepository postContentRepository, IMapper mapper)
        {
            _postContentRepository = postContentRepository;
            _mapper = mapper;
        }

        // Get ALL 
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PostContent>))]
        public IActionResult GetAllPostContent()
        {
            var postContents = _mapper.Map<List<PostContentDto>>(_postContentRepository.GetAllPostContent());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(postContents);
        }

        // Get By ID
        [HttpGet("{postContentId}")]
        [ProducesResponseType(200, Type = typeof(PostContent))]
        //[ProducesResponseType(400)]
        public IActionResult GetPostContent(int postContentId)
        {
            if (!_postContentRepository.PostContentExists(postContentId))
                return NotFound();

            var map = _mapper.Map<PostContentDto>(_postContentRepository.GetPostContent(postContentId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(map);
        }

        //Create
        [HttpPost]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        public IActionResult CreatePostContent([FromBody] PostContentDto create)
        {
            // id tự tăng nên không cần check Exitsts (suy nghỉ cá nhân thoi).
            if (create == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var map = _mapper.Map<PostContent>(create);

            if (!_postContentRepository.CreatePostContent(map))
            {
                ModelState.AddModelError("", "Something went wrong while create");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        // Update
        [HttpPut("{postContentId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult UpdateAction(int postContentId, [FromBody] PostContentDto updated)
        {
            if (updated == null)
                return BadRequest(ModelState);

            if (postContentId != updated.Id)
                return BadRequest(ModelState);

            if (!_postContentRepository.PostContentExists(postContentId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var map = _mapper.Map<PostContent>(updated);

            if (!_postContentRepository.UpdatePostContent(map))
            {
                ModelState.AddModelError("", "Something went wrong updating PostContent");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Updated");
        }

        // Delete
        [HttpDelete("{postContentId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult DeletePostContent(int postContentId)
        {
            if (!_postContentRepository.PostContentExists(postContentId))
            {
                return NotFound();
            }

            var delete = _postContentRepository.GetPostContent(postContentId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_postContentRepository.DeletePostContent(delete))
            {
                ModelState.AddModelError("", "Something went wrong deleting PostContent");
            }

            return Ok("Successfully Deleted");
        }
    }
}
