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
    public class AccountTypeController : ControllerBase
    {
        private readonly IAccountTypeRepository _accountTypeRepository;
        private readonly IMapper _mapper;

        public AccountTypeController(IAccountTypeRepository accountTypeRepository, IMapper mapper)
        {
            _accountTypeRepository = accountTypeRepository;
            _mapper = mapper;
        }

        // Get ALL 
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AccountType>))]
        public IActionResult GetAllAccountType()
        {
            var accountTypes = _mapper.Map<List<AccountTypeDto>>(_accountTypeRepository.GetAllAccountType());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(accountTypes);
        }

        // Get By ID
        [HttpGet("{accountTypeId}")]
        [ProducesResponseType(200, Type = typeof(AccountType))]
        //[ProducesResponseType(400)]
        public IActionResult GetAccountType(int accountTypeId)
        {
            if (!_accountTypeRepository.AccountTypeExists(accountTypeId))
                return NotFound();

            var map = _mapper.Map<AccountTypeDto>(_accountTypeRepository.GetAccountType(accountTypeId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(map);
        }

        //Create
        [HttpPost]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        public IActionResult CreateAccountType([FromBody] AccountTypeDto create)
        {
            // id tự tăng nên không cần check Exitsts (suy nghỉ cá nhân thoi).
            if (create == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var map = _mapper.Map<AccountType>(create);

            if (!_accountTypeRepository.CreateAccountType(map))
            {
                ModelState.AddModelError("", "Something went wrong while create");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        // Update
        [HttpPut("{accountTypeId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult UpdateAccountType(int accountTypeId, [FromBody] AccountTypeDto updated)
        {
            if (updated == null)
                return BadRequest(ModelState);

            if (accountTypeId != updated.Id)
                return BadRequest(ModelState);

            if (!_accountTypeRepository.AccountTypeExists(accountTypeId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var map = _mapper.Map<AccountType>(updated);

            if (!_accountTypeRepository.UpdateAccountType(map))
            {
                ModelState.AddModelError("", "Something went wrong updating AccountType");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Updated");
        }

        // Delete
        [HttpDelete("{accountTypeId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult DeleteAccountType(int accountTypeId)
        {
            if (!_accountTypeRepository.AccountTypeExists(accountTypeId))
            {
                return NotFound();
            }

            var delete = _accountTypeRepository.GetAccountType(accountTypeId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_accountTypeRepository.DeleteAccountType(delete))
            {
                ModelState.AddModelError("", "Something went wrong deleting AccountType");
            }

            return Ok("Successfully Deleted");
        }
    }
}
