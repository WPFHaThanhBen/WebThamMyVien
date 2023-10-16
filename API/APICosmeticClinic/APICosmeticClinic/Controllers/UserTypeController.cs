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
    public class UserTypeController : ControllerBase
    {
        private readonly IUserTypeRepository _userTypeRepository;
        private readonly IMapper _mapper;

        public UserTypeController(IUserTypeRepository userTypeRepository, IMapper mapper)
        {
            _userTypeRepository = userTypeRepository;
            _mapper = mapper;
        }

        // Get ALL 
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserType>))]
        public IActionResult GetAllUserType()
        {
            var userTypes = _mapper.Map<List<UserTypeDto>>(_userTypeRepository.GetAllUserType());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(userTypes);
        }

        // Get By ID
        [HttpGet("{userTypeId}")]
        [ProducesResponseType(200, Type = typeof(UserType))]
        //[ProducesResponseType(400)]
        public IActionResult GetUserType(int userTypeId)
        {
            if (!_userTypeRepository.UserTypeExists(userTypeId))
                return NotFound();

            var map = _mapper.Map<UserTypeDto>(_userTypeRepository.GetUserType(userTypeId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(map);
        }

        //Create
        [HttpPost]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        public IActionResult CreateUserType([FromBody] UserTypeDto create)
        {
            // id tự tăng nên không cần check Exitsts (suy nghỉ cá nhân thoi).
            if (create == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var map = _mapper.Map<UserType>(create);

            if (!_userTypeRepository.CreateUserType(map))
            {
                ModelState.AddModelError("", "Something went wrong while create");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        // Update
        [HttpPut("{userTypeId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult UpdateAction(int userTypeId, [FromBody] UserTypeDto updated)
        {
            if (updated == null)
                return BadRequest(ModelState);

            if (userTypeId != updated.Id)
                return BadRequest(ModelState);

            if (!_userTypeRepository.UserTypeExists(userTypeId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var map = _mapper.Map<UserType>(updated);

            if (!_userTypeRepository.UpdateUserType(map))
            {
                ModelState.AddModelError("", "Something went wrong updating UserType");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Updated");
        }

        // Delete
        [HttpDelete("{userTypeId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult DeleteUserType(int userTypeId)
        {
            if (!_userTypeRepository.UserTypeExists(userTypeId))
            {
                return NotFound();
            }

            var delete = _userTypeRepository.GetUserType(userTypeId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_userTypeRepository.DeleteUserType(delete))
            {
                ModelState.AddModelError("", "Something went wrong deleting UserType");
            }

            return Ok("Successfully Deleted");
        }
    }
}
