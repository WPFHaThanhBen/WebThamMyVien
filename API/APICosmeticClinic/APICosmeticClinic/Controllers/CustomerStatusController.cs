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
    public class CustomerStatusController : ControllerBase
    {
        private readonly ICustomerStatusRepository _customerStatusRepository;
        private readonly IMapper _mapper;

        public CustomerStatusController(ICustomerStatusRepository customerStatusRepository, IMapper mapper)
        {
            _customerStatusRepository = customerStatusRepository;
            _mapper = mapper;
        }

        // Get ALL 
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CustomerStatus>))]
        public IActionResult GetAllCustomerStatus()
        {
            var customerStatuss = _mapper.Map<List<CustomerStatusDto>>(_customerStatusRepository.GetAllCustomerStatus());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(customerStatuss);
        }

        // Get By ID
        [HttpGet("{customerStatusId}")]
        [ProducesResponseType(200, Type = typeof(CustomerStatus))]
        //[ProducesResponseType(400)]
        public IActionResult GetCustomerStatus(int customerStatusId)
        {
            if (!_customerStatusRepository.CustomerStatusExists(customerStatusId))
                return NotFound();

            var map = _mapper.Map<CustomerStatusDto>(_customerStatusRepository.GetCustomerStatus(customerStatusId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(map);
        }

        //Create
        [HttpPost]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        public IActionResult CreateCustomerStatus([FromBody] CustomerStatusDto create)
        {
            // id tự tăng nên không cần check Exitsts (suy nghỉ cá nhân thoi).
            if (create == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var map = _mapper.Map<CustomerStatus>(create);

            if (!_customerStatusRepository.CreateCustomerStatus(map))
            {
                ModelState.AddModelError("", "Something went wrong while create");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        // Update
        [HttpPut("{customerStatusId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult UpdateAction(int customerStatusId, [FromBody] CustomerStatusDto updated)
        {
            if (updated == null)
                return BadRequest(ModelState);

            if (customerStatusId != updated.Id)
                return BadRequest(ModelState);

            if (!_customerStatusRepository.CustomerStatusExists(customerStatusId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var map = _mapper.Map<CustomerStatus>(updated);

            if (!_customerStatusRepository.UpdateCustomerStatus(map))
            {
                ModelState.AddModelError("", "Something went wrong updating CustomerStatus");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Updated");
        }

        // Delete
        [HttpDelete("{customerStatusId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult DeleteCustomerStatus(int customerStatusId)
        {
            if (!_customerStatusRepository.CustomerStatusExists(customerStatusId))
            {
                return NotFound();
            }

            var delete = _customerStatusRepository.GetCustomerStatus(customerStatusId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_customerStatusRepository.DeleteCustomerStatus(delete))
            {
                ModelState.AddModelError("", "Something went wrong deleting CustomerStatus");
            }

            return Ok("Successfully Deleted");
        }
    }
}
