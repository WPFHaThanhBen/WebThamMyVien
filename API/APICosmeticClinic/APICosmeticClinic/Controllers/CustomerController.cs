using APICosmeticClinic.Dto;
using APICosmeticClinic.Interfaces;
using APICosmeticClinic.Models;
using APICosmeticClinic.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APICosmeticClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        // Get ALL 
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Customer>))]
        public IActionResult GetAllCustomer()
        {
            var customers = _mapper.Map<List<CustomerDto>>(_customerRepository.GetAllCustomer());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(customers);
        }
        // Get ALL 
        [HttpGet("Skip/{start}/{skip}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Customer>))]
        public IActionResult GetAllCustomerSkip(int start, int skip)
        {
            var customers = _mapper.Map<List<CustomerDto>>(_customerRepository.GetAllCustomerSkip(start,skip));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(customers);
        }

        // Get By ID
        [HttpGet("{customerId}")]
        [ProducesResponseType(200, Type = typeof(Customer))]
        //[ProducesResponseType(400)]
        public IActionResult GetCustomer(int customerId)
        {
            if (!_customerRepository.CustomerExists(customerId))
                return NotFound();

            var map = _mapper.Map<CustomerDto>(_customerRepository.GetCustomer(customerId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(map);
        }
		
        // Get By ID
		[HttpGet("Final")]
		[ProducesResponseType(200, Type = typeof(Invoice))]
		//[ProducesResponseType(400)]
		public IActionResult GetCustomer()
		{
			var map = _mapper.Map<CustomerDto>(_customerRepository.GetCustomerFinal());

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(map);
		}
		// Get By SDT
		[HttpGet("CustomerBySDT/{SDT}")]
        [ProducesResponseType(200, Type = typeof(Customer))]
        //[ProducesResponseType(400)]
        public IActionResult GetCustomerBySDT(string SDT)
        {
            if (!_customerRepository.CustomerExistsBySDT(SDT))
                return NotFound();

            var map = _mapper.Map<CustomerDto>(_customerRepository.GetCustomerBySDT(SDT));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(map);
        }

        //Create
        [HttpPost]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        public IActionResult CreateCustomer([FromBody] CustomerDto create)
        {
            // id tự tăng nên không cần check Exitsts (suy nghỉ cá nhân thoi).
            if (create == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var map = _mapper.Map<Customer>(create);

            if (!_customerRepository.CreateCustomer(map))
            {
                ModelState.AddModelError("", "Something went wrong while create");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        // Update
        [HttpPut("{customerId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult UpdateCustomer(int customerId, [FromBody] CustomerDto updated)
        {
            if (updated == null)
                return BadRequest(ModelState);

            if (customerId != updated.Id)
                return BadRequest(ModelState);

            if (!_customerRepository.CustomerExists(customerId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var map = _mapper.Map<Customer>(updated);

            if (!_customerRepository.UpdateCustomer(map))
            {
                ModelState.AddModelError("", "Something went wrong updating Customer");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Updated");
        }

        // Delete
        [HttpDelete("{customerId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult DeleteCustomer(int customerId)
        {
            if (!_customerRepository.CustomerExists(customerId))
            {
                return NotFound();
            }

            var delete = _customerRepository.GetCustomer(customerId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_customerRepository.DeleteCustomer(delete))
            {
                ModelState.AddModelError("", "Something went wrong deleting Customer");
            }

            return Ok("Successfully Deleted");
        }
    }
}
