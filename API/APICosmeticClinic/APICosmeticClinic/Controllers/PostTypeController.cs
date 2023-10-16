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
    public class PostTypeController : ControllerBase
    {
        private readonly IPostTypeRepository _postTypeRepository;
        private readonly IMapper _mapper;

        public PostTypeController(IPostTypeRepository postTypeRepository, IMapper mapper)
        {
            _postTypeRepository = postTypeRepository;
            _mapper = mapper;
        }

        // Get ALL 
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PostType>))]
        public IActionResult GetAllPostType()
        {
            var postTypes = _mapper.Map<List<PostTypeDto>>(_postTypeRepository.GetAllPostType());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(postTypes);
        }

        // Get By ID
        [HttpGet("{postTypeId}")]
        [ProducesResponseType(200, Type = typeof(PostType))]
        //[ProducesResponseType(400)]
        public IActionResult GetPostType(int postTypeId)
        {
            if (!_postTypeRepository.PostTypeExists(postTypeId))
                return NotFound();

            var map = _mapper.Map<PostTypeDto>(_postTypeRepository.GetPostType(postTypeId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(map);
        }

        //Create
        [HttpPost]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        public IActionResult CreatePostType([FromBody] PostTypeDto create)
        {
            // id tự tăng nên không cần check Exitsts (suy nghỉ cá nhân thoi).
            if (create == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var map = _mapper.Map<PostType>(create);

            if (!_postTypeRepository.CreatePostType(map))
            {
                ModelState.AddModelError("", "Something went wrong while create");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        // Update
        [HttpPut("{postTypeId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult UpdateAction(int postTypeId, [FromBody] PostTypeDto updated)
        {
            if (updated == null)
                return BadRequest(ModelState);

            if (postTypeId != updated.Id)
                return BadRequest(ModelState);

            if (!_postTypeRepository.PostTypeExists(postTypeId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var map = _mapper.Map<PostType>(updated);

            if (!_postTypeRepository.UpdatePostType(map))
            {
                ModelState.AddModelError("", "Something went wrong updating PostType");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Updated");
        }

        // Delete
        [HttpDelete("{postTypeId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult DeletePostType(int postTypeId)
        {
            if (!_postTypeRepository.PostTypeExists(postTypeId))
            {
                return NotFound();
            }

            var delete = _postTypeRepository.GetPostType(postTypeId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_postTypeRepository.DeletePostType(delete))
            {
                ModelState.AddModelError("", "Something went wrong deleting PostType");
            }

            return Ok("Successfully Deleted");
        }
    }
}
