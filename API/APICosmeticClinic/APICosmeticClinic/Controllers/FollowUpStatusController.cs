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
    public class FollowUpStatusController : ControllerBase
    {
        private readonly IFollowUpStatusRepository _followUpStatusRepository;
        private readonly IMapper _mapper;

        public FollowUpStatusController(IFollowUpStatusRepository followUpStatusRepository, IMapper mapper)
        {
            _followUpStatusRepository = followUpStatusRepository;
            _mapper = mapper;
        }

        // Get ALL 
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<FollowUpStatus>))]
        public IActionResult GetAllFollowUpStatus()
        {
            var followUpStatuss = _mapper.Map<List<FollowUpStatusDto>>(_followUpStatusRepository.GetAllFollowUpStatus());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(followUpStatuss);
        }

        // Get By ID
        [HttpGet("{followUpStatusId}")]
        [ProducesResponseType(200, Type = typeof(FollowUpStatus))]
        //[ProducesResponseType(400)]
        public IActionResult GetFollowUpStatus(int followUpStatusId)
        {
            if (!_followUpStatusRepository.FollowUpStatusExists(followUpStatusId))
                return NotFound();

            var map = _mapper.Map<FollowUpStatusDto>(_followUpStatusRepository.GetFollowUpStatus(followUpStatusId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(map);
        }

        //Create
        [HttpPost]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        public IActionResult CreateFollowUpStatus([FromBody] FollowUpStatusDto create)
        {
            // id tự tăng nên không cần check Exitsts (suy nghỉ cá nhân thoi).
            if (create == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var map = _mapper.Map<FollowUpStatus>(create);

            if (!_followUpStatusRepository.CreateFollowUpStatus(map))
            {
                ModelState.AddModelError("", "Something went wrong while create");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        // Update
        [HttpPut("{followUpStatusId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult UpdateAction(int followUpStatusId, [FromBody] FollowUpStatusDto updated)
        {
            if (updated == null)
                return BadRequest(ModelState);

            if (followUpStatusId != updated.Id)
                return BadRequest(ModelState);

            if (!_followUpStatusRepository.FollowUpStatusExists(followUpStatusId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var map = _mapper.Map<FollowUpStatus>(updated);

            if (!_followUpStatusRepository.UpdateFollowUpStatus(map))
            {
                ModelState.AddModelError("", "Something went wrong updating FollowUpStatus");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Updated");
        }

        // Delete
        [HttpDelete("{followUpStatusId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult DeleteFollowUpStatus(int followUpStatusId)
        {
            if (!_followUpStatusRepository.FollowUpStatusExists(followUpStatusId))
            {
                return NotFound();
            }

            var delete = _followUpStatusRepository.GetFollowUpStatus(followUpStatusId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_followUpStatusRepository.DeleteFollowUpStatus(delete))
            {
                ModelState.AddModelError("", "Something went wrong deleting FollowUpStatus");
            }

            return Ok("Successfully Deleted");
        }
    }
}
