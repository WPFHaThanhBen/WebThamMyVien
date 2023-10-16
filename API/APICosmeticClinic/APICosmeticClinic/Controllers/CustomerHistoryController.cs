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
    public class CustomerHistoryController : ControllerBase
    {
        private readonly ICustomerHistoryRepository _customerHistoryRepository;
        private readonly IMapper _mapper;

        public CustomerHistoryController(ICustomerHistoryRepository customerHistoryRepository, IMapper mapper)
        {
            _customerHistoryRepository = customerHistoryRepository;
            _mapper = mapper;
        }

        // Get ALL 
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CustomerHistory>))]
        public IActionResult GetAllCustomerHistory()
        {
            var customerHistorys = _mapper.Map<List<CustomerHistoryDto>>(_customerHistoryRepository.GetAllCustomerHistory());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(customerHistorys);
        }

        // Get By ID
        [HttpGet("{customerHistoryId}")]
        [ProducesResponseType(200, Type = typeof(CustomerHistory))]
        //[ProducesResponseType(400)]
        public IActionResult GetCustomerHistory(int customerHistoryId)
        {
            if (!_customerHistoryRepository.CustomerHistoryExists(customerHistoryId))
                return NotFound();

            var map = _mapper.Map<CustomerHistoryDto>(_customerHistoryRepository.GetCustomerHistory(customerHistoryId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(map);
        }

        //Create
        [HttpPost]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        public IActionResult CreateCustomerHistory([FromBody] CustomerHistoryDto create)
        {
            // id tự tăng nên không cần check Exitsts (suy nghỉ cá nhân thoi).
            if (create == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var map = _mapper.Map<CustomerHistory>(create);

            if (!_customerHistoryRepository.CreateCustomerHistory(map))
            {
                ModelState.AddModelError("", "Something went wrong while create");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        // Update
        [HttpPut("{customerHistoryId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult UpdateAction(int customerHistoryId, [FromBody] CustomerHistoryDto updated)
        {
            if (updated == null)
                return BadRequest(ModelState);

            if (customerHistoryId != updated.Id)
                return BadRequest(ModelState);

            if (!_customerHistoryRepository.CustomerHistoryExists(customerHistoryId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var map = _mapper.Map<CustomerHistory>(updated);

            if (!_customerHistoryRepository.UpdateCustomerHistory(map))
            {
                ModelState.AddModelError("", "Something went wrong updating CustomerHistory");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Updated");
        }

        // Delete
        [HttpDelete("{customerHistoryId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult DeleteCustomerHistory(int customerHistoryId)
        {
            if (!_customerHistoryRepository.CustomerHistoryExists(customerHistoryId))
            {
                return NotFound();
            }

            var delete = _customerHistoryRepository.GetCustomerHistory(customerHistoryId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_customerHistoryRepository.DeleteCustomerHistory(delete))
            {
                ModelState.AddModelError("", "Something went wrong deleting CustomerHistory");
            }

            return Ok("Successfully Deleted");
        }
    }
}
