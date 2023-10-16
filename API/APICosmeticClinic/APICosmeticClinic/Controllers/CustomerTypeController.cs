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
    public class CustomerTypeController : ControllerBase
    {
        private readonly ICustomerTypeRepository _customerTypeRepository;
        private readonly IMapper _mapper;

        public CustomerTypeController(ICustomerTypeRepository customerTypeRepository, IMapper mapper)
        {
            _customerTypeRepository = customerTypeRepository;
            _mapper = mapper;
        }

        // Get ALL 
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CustomerType>))]
        public IActionResult GetAllCustomerType()
        {
            var customerTypes = _mapper.Map<List<CustomerTypeDto>>(_customerTypeRepository.GetAllCustomerType());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(customerTypes);
        }

        // Get By ID
        [HttpGet("{customerTypeId}")]
        [ProducesResponseType(200, Type = typeof(CustomerType))]
        //[ProducesResponseType(400)]
        public IActionResult GetCustomerType(int customerTypeId)
        {
            if (!_customerTypeRepository.CustomerTypeExists(customerTypeId))
                return NotFound();

            var map = _mapper.Map<CustomerTypeDto>(_customerTypeRepository.GetCustomerType(customerTypeId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(map);
        }

        //Create
        [HttpPost]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        public IActionResult CreateCustomerType([FromBody] CustomerTypeDto create)
        {
            // id tự tăng nên không cần check Exitsts (suy nghỉ cá nhân thoi).
            if (create == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var map = _mapper.Map<CustomerType>(create);

            if (!_customerTypeRepository.CreateCustomerType(map))
            {
                ModelState.AddModelError("", "Something went wrong while create");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        // Update
        [HttpPut("{customerTypeId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult UpdateAction(int customerTypeId, [FromBody] CustomerTypeDto updated)
        {
            if (updated == null)
                return BadRequest(ModelState);

            if (customerTypeId != updated.Id)
                return BadRequest(ModelState);

            if (!_customerTypeRepository.CustomerTypeExists(customerTypeId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var map = _mapper.Map<CustomerType>(updated);

            if (!_customerTypeRepository.UpdateCustomerType(map))
            {
                ModelState.AddModelError("", "Something went wrong updating CustomerType");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Updated");
        }

        // Delete
        [HttpDelete("{customerTypeId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult DeleteCustomerType(int customerTypeId)
        {
            if (!_customerTypeRepository.CustomerTypeExists(customerTypeId))
            {
                return NotFound();
            }

            var delete = _customerTypeRepository.GetCustomerType(customerTypeId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_customerTypeRepository.DeleteCustomerType(delete))
            {
                ModelState.AddModelError("", "Something went wrong deleting CustomerType");
            }

            return Ok("Successfully Deleted");
        }
    }
}
