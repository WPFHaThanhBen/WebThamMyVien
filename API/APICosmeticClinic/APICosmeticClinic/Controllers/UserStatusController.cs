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
    public class UserStatusController : ControllerBase
    {
        private readonly IUserStatusRepository _userStatusRepository;
        private readonly IMapper _mapper;

        public UserStatusController(IUserStatusRepository userStatusRepository, IMapper mapper)
        {
            _userStatusRepository = userStatusRepository;
            _mapper = mapper;
        }

        // Get ALL 
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserStatus>))]
        public IActionResult GetAllUserStatus()
        {
            var userStatuss = _mapper.Map<List<UserStatusDto>>(_userStatusRepository.GetAllUserStatus());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(userStatuss);
        }

        // Get By ID
        [HttpGet("{userStatusId}")]
        [ProducesResponseType(200, Type = typeof(UserStatus))]
        //[ProducesResponseType(400)]
        public IActionResult GetUserStatus(int userStatusId)
        {
            if (!_userStatusRepository.UserStatusExists(userStatusId))
                return NotFound();

            var map = _mapper.Map<UserStatusDto>(_userStatusRepository.GetUserStatus(userStatusId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(map);
        }

        //Create
        [HttpPost]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        public IActionResult CreateUserStatus([FromBody] UserStatusDto create)
        {
            // id tự tăng nên không cần check Exitsts (suy nghỉ cá nhân thoi).
            if (create == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var map = _mapper.Map<UserStatus>(create);

            if (!_userStatusRepository.CreateUserStatus(map))
            {
                ModelState.AddModelError("", "Something went wrong while create");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        // Update
        [HttpPut("{userStatusId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult UpdateAction(int userStatusId, [FromBody] UserStatusDto updated)
        {
            if (updated == null)
                return BadRequest(ModelState);

            if (userStatusId != updated.Id)
                return BadRequest(ModelState);

            if (!_userStatusRepository.UserStatusExists(userStatusId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var map = _mapper.Map<UserStatus>(updated);

            if (!_userStatusRepository.UpdateUserStatus(map))
            {
                ModelState.AddModelError("", "Something went wrong updating UserStatus");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Updated");
        }

        // Delete
        [HttpDelete("{userStatusId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult DeleteUserStatus(int userStatusId)
        {
            if (!_userStatusRepository.UserStatusExists(userStatusId))
            {
                return NotFound();
            }

            var delete = _userStatusRepository.GetUserStatus(userStatusId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_userStatusRepository.DeleteUserStatus(delete))
            {
                ModelState.AddModelError("", "Something went wrong deleting UserStatus");
            }

            return Ok("Successfully Deleted");
        }
    }
}
