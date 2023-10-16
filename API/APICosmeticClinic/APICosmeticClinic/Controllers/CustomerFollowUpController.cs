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
    public class CustomerFollowUpController : ControllerBase
    {
        private readonly ICustomerFollowUpRepository _customerFollowUpRepository;
        private readonly IMapper _mapper;

        public CustomerFollowUpController(ICustomerFollowUpRepository customerFollowUpRepository, IMapper mapper)
        {
            _customerFollowUpRepository = customerFollowUpRepository;
            _mapper = mapper;
        }

        // Get ALL 
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CustomerFollowUp>))]
        public IActionResult GetAllCustomerFollowUp()
        {
            var customerFollowUps = _mapper.Map<List<CustomerFollowUpDto>>(_customerFollowUpRepository.GetAllCustomerFollowUp());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(customerFollowUps);
        }

        // Get By ID
        [HttpGet("{customerFollowUpId}")]
        [ProducesResponseType(200, Type = typeof(CustomerFollowUp))]
        //[ProducesResponseType(400)]
        public IActionResult GetCustomerFollowUp(int customerFollowUpId)
        {
            if (!_customerFollowUpRepository.CustomerFollowUpExists(customerFollowUpId))
                return NotFound();

            var map = _mapper.Map<CustomerFollowUpDto>(_customerFollowUpRepository.GetCustomerFollowUp(customerFollowUpId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(map);
        }

        //Create
        [HttpPost]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        public IActionResult CreateCustomerFollowUp([FromBody] CustomerFollowUpDto create)
        {
            // id tự tăng nên không cần check Exitsts (suy nghỉ cá nhân thoi).
            if (create == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var map = _mapper.Map<CustomerFollowUp>(create);

            if (!_customerFollowUpRepository.CreateCustomerFollowUp(map))
            {
                ModelState.AddModelError("", "Something went wrong while create");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        // Update
        [HttpPut("{customerFollowUpId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult UpdateAction(int customerFollowUpId, [FromBody] CustomerFollowUpDto updated)
        {
            if (updated == null)
                return BadRequest(ModelState);

            if (customerFollowUpId != updated.Id)
                return BadRequest(ModelState);

            if (!_customerFollowUpRepository.CustomerFollowUpExists(customerFollowUpId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var map = _mapper.Map<CustomerFollowUp>(updated);

            if (!_customerFollowUpRepository.UpdateCustomerFollowUp(map))
            {
                ModelState.AddModelError("", "Something went wrong updating CustomerFollowUp");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Updated");
        }

        // Delete
        [HttpDelete("{customerFollowUpId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult DeleteCustomerFollowUp(int customerFollowUpId)
        {
            if (!_customerFollowUpRepository.CustomerFollowUpExists(customerFollowUpId))
            {
                return NotFound();
            }

            var delete = _customerFollowUpRepository.GetCustomerFollowUp(customerFollowUpId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_customerFollowUpRepository.DeleteCustomerFollowUp(delete))
            {
                ModelState.AddModelError("", "Something went wrong deleting CustomerFollowUp");
            }

            return Ok("Successfully Deleted");
        }
    }
}
