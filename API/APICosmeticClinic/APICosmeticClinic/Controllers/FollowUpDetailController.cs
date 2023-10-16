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
    public class FollowUpDetailController : ControllerBase
    {
        private readonly IFollowUpDetailRepository _followUpDetailRepository;
        private readonly IMapper _mapper;

        public FollowUpDetailController(IFollowUpDetailRepository followUpDetailRepository, IMapper mapper)
        {
            _followUpDetailRepository = followUpDetailRepository;
            _mapper = mapper;
        }

        // Get ALL 
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<FollowUpDetail>))]
        public IActionResult GetAllFollowUpDetail()
        {
            var followUpDetails = _mapper.Map<List<FollowUpDetailDto>>(_followUpDetailRepository.GetAllFollowUpDetail());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(followUpDetails);
        }

        // Get By ID
        [HttpGet("{followUpDetailId}")]
        [ProducesResponseType(200, Type = typeof(FollowUpDetail))]
        //[ProducesResponseType(400)]
        public IActionResult GetFollowUpDetail(int followUpDetailId)
        {
            if (!_followUpDetailRepository.FollowUpDetailExists(followUpDetailId))
                return NotFound();

            var map = _mapper.Map<FollowUpDetailDto>(_followUpDetailRepository.GetFollowUpDetail(followUpDetailId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(map);
        }

        //Create
        [HttpPost]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        public IActionResult CreateFollowUpDetail([FromBody] FollowUpDetailDto create)
        {
            // id tự tăng nên không cần check Exitsts (suy nghỉ cá nhân thoi).
            if (create == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var map = _mapper.Map<FollowUpDetail>(create);

            if (!_followUpDetailRepository.CreateFollowUpDetail(map))
            {
                ModelState.AddModelError("", "Something went wrong while create");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        // Update
        [HttpPut("{followUpDetailId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult UpdateAction(int followUpDetailId, [FromBody] FollowUpDetailDto updated)
        {
            if (updated == null)
                return BadRequest(ModelState);

            if (followUpDetailId != updated.Id)
                return BadRequest(ModelState);

            if (!_followUpDetailRepository.FollowUpDetailExists(followUpDetailId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var map = _mapper.Map<FollowUpDetail>(updated);

            if (!_followUpDetailRepository.UpdateFollowUpDetail(map))
            {
                ModelState.AddModelError("", "Something went wrong updating FollowUpDetail");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Updated");
        }

        // Delete
        [HttpDelete("{followUpDetailId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult DeleteFollowUpDetail(int followUpDetailId)
        {
            if (!_followUpDetailRepository.FollowUpDetailExists(followUpDetailId))
            {
                return NotFound();
            }

            var delete = _followUpDetailRepository.GetFollowUpDetail(followUpDetailId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_followUpDetailRepository.DeleteFollowUpDetail(delete))
            {
                ModelState.AddModelError("", "Something went wrong deleting FollowUpDetail");
            }

            return Ok("Successfully Deleted");
        }
    }
}
