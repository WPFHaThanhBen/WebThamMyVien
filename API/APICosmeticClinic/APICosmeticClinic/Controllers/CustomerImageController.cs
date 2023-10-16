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
    public class CustomerImageController : ControllerBase
    {
        private readonly ICustomerImageRepository _customerImageRepository;
        private readonly IMapper _mapper;

        public CustomerImageController(ICustomerImageRepository customerImageRepository, IMapper mapper)
        {
            _customerImageRepository = customerImageRepository;
            _mapper = mapper;
        }

        // Get ALL 
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CustomerImage>))]
        public IActionResult GetAllCustomerImage()
        {
            var customerImages = _mapper.Map<List<CustomerImageDto>>(_customerImageRepository.GetAllCustomerImage());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(customerImages);
        }

        // Get By ID
        [HttpGet("{customerImageId}")]
        [ProducesResponseType(200, Type = typeof(CustomerImage))]
        //[ProducesResponseType(400)]
        public IActionResult GetCustomerImage(int customerImageId)
        {
            if (!_customerImageRepository.CustomerImageExists(customerImageId))
                return NotFound();

            var map = _mapper.Map<CustomerImageDto>(_customerImageRepository.GetCustomerImage(customerImageId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(map);
        }

        //Create
        [HttpPost]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        public IActionResult CreateCustomerImage([FromBody] CustomerImageDto create)
        {
            // id tự tăng nên không cần check Exitsts (suy nghỉ cá nhân thoi).
            if (create == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var map = _mapper.Map<CustomerImage>(create);

            if (!_customerImageRepository.CreateCustomerImage(map))
            {
                ModelState.AddModelError("", "Something went wrong while create");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        // Update
        [HttpPut("{customerImageId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult UpdateAction(int customerImageId, [FromBody] CustomerImageDto updated)
        {
            if (updated == null)
                return BadRequest(ModelState);

            if (customerImageId != updated.Id)
                return BadRequest(ModelState);

            if (!_customerImageRepository.CustomerImageExists(customerImageId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var map = _mapper.Map<CustomerImage>(updated);

            if (!_customerImageRepository.UpdateCustomerImage(map))
            {
                ModelState.AddModelError("", "Something went wrong updating CustomerImage");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Updated");
        }

        // Delete
        [HttpDelete("{customerImageId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult DeleteCustomerImage(int customerImageId)
        {
            if (!_customerImageRepository.CustomerImageExists(customerImageId))
            {
                return NotFound();
            }

            var delete = _customerImageRepository.GetCustomerImage(customerImageId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_customerImageRepository.DeleteCustomerImage(delete))
            {
                ModelState.AddModelError("", "Something went wrong deleting CustomerImage");
            }

            return Ok("Successfully Deleted");
        }
    }
}
