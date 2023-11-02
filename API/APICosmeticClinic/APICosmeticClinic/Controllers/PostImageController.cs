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
    public class PostImageController : ControllerBase
    {
        private readonly IPostImageRepository _postImageRepository;
        private readonly IMapper _mapper;

        public PostImageController(IPostImageRepository postImageRepository, IMapper mapper)
        {
            _postImageRepository = postImageRepository;
            _mapper = mapper;
        }

        // Get ALL 
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PostImage>))]
        public IActionResult GetAllPostImage()
        {
            var postImages = _mapper.Map<List<PostImageDto>>(_postImageRepository.GetAllPostImage());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(postImages);
        }
		// Get ALL 
		[HttpGet("PostImageByPost/{postId}")]
		[ProducesResponseType(200, Type = typeof(IEnumerable<PostImage>))]
		public IActionResult GetAllPostImage(int postId)
		{
			var postImages = _mapper.Map<List<PostImageDto>>(_postImageRepository.GetAllPostImageByPost(postId));

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(postImages);
		}
		// Get By ID
		[HttpGet("{postImageId}")]
        [ProducesResponseType(200, Type = typeof(PostImage))]
        //[ProducesResponseType(400)]
        public IActionResult GetPostImage(int postImageId)
        {
            if (!_postImageRepository.PostImageExists(postImageId))
                return NotFound();

            var map = _mapper.Map<PostImageDto>(_postImageRepository.GetPostImage(postImageId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(map);
        }

        //Create
        [HttpPost]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        public IActionResult CreatePostImage([FromBody] PostImageDto create)
        {
            // id tự tăng nên không cần check Exitsts (suy nghỉ cá nhân thoi).
            if (create == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var map = _mapper.Map<PostImage>(create);

            if (!_postImageRepository.CreatePostImage(map))
            {
                ModelState.AddModelError("", "Something went wrong while create");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        // Update
        [HttpPut("{postImageId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult UpdateAction(int postImageId, [FromBody] PostImageDto updated)
        {
            if (updated == null)
                return BadRequest(ModelState);

            if (postImageId != updated.Id)
                return BadRequest(ModelState);

            if (!_postImageRepository.PostImageExists(postImageId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var map = _mapper.Map<PostImage>(updated);

            if (!_postImageRepository.UpdatePostImage(map))
            {
                ModelState.AddModelError("", "Something went wrong updating PostImage");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Updated");
        }

        // Delete
        [HttpDelete("{postImageId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult DeletePostImage(int postImageId)
        {
            if (!_postImageRepository.PostImageExists(postImageId))
            {
                return NotFound();
            }

            var delete = _postImageRepository.GetPostImage(postImageId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_postImageRepository.DeletePostImage(delete))
            {
                ModelState.AddModelError("", "Something went wrong deleting PostImage");
            }

            return Ok("Successfully Deleted");
        }
    }
}
