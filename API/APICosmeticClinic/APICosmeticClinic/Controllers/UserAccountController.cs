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
    public class UserAccountController : ControllerBase
    {
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly IMapper _mapper;

        public UserAccountController(IUserAccountRepository userAccountRepository, IMapper mapper)
        {
            _userAccountRepository = userAccountRepository;
            _mapper = mapper;
        }

        // Get ALL 
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserAccount>))]
        public IActionResult GetAllUserAccount()
        {
            var userAccounts = _mapper.Map<List<UserAccountDto>>(_userAccountRepository.GetAllUserAccount());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(userAccounts);
        }

        // Get By ID
        [HttpGet("{userAccountId}")]
        [ProducesResponseType(200, Type = typeof(UserAccount))]
        //[ProducesResponseType(400)]
        public IActionResult GetUserAccount(int userAccountId)
        {
            if (!_userAccountRepository.UserAccountExists(userAccountId))
                return NotFound();

            var map = _mapper.Map<UserAccountDto>(_userAccountRepository.GetUserAccount(userAccountId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(map);
        }


        // Get By ID
        [HttpGet("GetUserAccountByUserName/{userName}")]
        [ProducesResponseType(200, Type = typeof(UserAccount))]
        //[ProducesResponseType(400)]
        public IActionResult GetUserAccount1(string userName)
        {
            var map = _mapper.Map<UserAccountDto>(_userAccountRepository.GetUserAccountByUserName(userName));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(map);
        }


        [HttpGet("Login/{userName}/{passWord}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserAccount>))]
        public IActionResult GetAllUserAccount1(string userName, string passWord)
        {
            var userAccounts = _userAccountRepository.LoginAccount(userName, passWord);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(userAccounts);
        }


        //Create
        [HttpPost]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        public IActionResult CreateUserAccount([FromBody] UserAccountDto create)
        {
            // id tự tăng nên không cần check Exitsts (suy nghỉ cá nhân thoi).
            if (create == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var map = _mapper.Map<UserAccount>(create);

            if (!_userAccountRepository.CreateUserAccount(map))
            {
                ModelState.AddModelError("", "Something went wrong while create");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        // Update
        [HttpPut("{userAccountId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult UpdateAction(int userAccountId, [FromBody] UserAccountDto updated)
        {
            if (updated == null)
                return BadRequest(ModelState);

            if (userAccountId != updated.Id)
                return BadRequest(ModelState);

            if (!_userAccountRepository.UserAccountExists(userAccountId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var map = _mapper.Map<UserAccount>(updated);

            if (!_userAccountRepository.UpdateUserAccount(map))
            {
                ModelState.AddModelError("", "Something went wrong updating UserAccount");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Updated");
        }

        // Delete
        [HttpDelete("{userAccountId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult DeleteUserAccount(int userAccountId)
        {
            if (!_userAccountRepository.UserAccountExists(userAccountId))
            {
                return NotFound();
            }

            var delete = _userAccountRepository.GetUserAccount(userAccountId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_userAccountRepository.DeleteUserAccount(delete))
            {
                ModelState.AddModelError("", "Something went wrong deleting UserAccount");
            }

            return Ok("Successfully Deleted");
        }

    }
}
