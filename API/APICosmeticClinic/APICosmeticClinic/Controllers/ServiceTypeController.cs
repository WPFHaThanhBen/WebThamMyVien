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
    public class ServiceTypeController : ControllerBase
    {
        private readonly IServiceTypeRepository _serviceTypeRepository;
        private readonly IMapper _mapper;

        public ServiceTypeController(IServiceTypeRepository serviceTypeRepository, IMapper mapper)
        {
            _serviceTypeRepository = serviceTypeRepository;
            _mapper = mapper;
        }

        // Get ALL 
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ServiceType>))]
        public IActionResult GetAllServiceType()
        {
            var serviceTypes = _mapper.Map<List<ServiceTypeDto>>(_serviceTypeRepository.GetAllServiceType());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(serviceTypes);
        }

        // Get By ID
        [HttpGet("{serviceTypeId}")]
        [ProducesResponseType(200, Type = typeof(ServiceType))]
        //[ProducesResponseType(400)]
        public IActionResult GetServiceType(int serviceTypeId)
        {
            if (!_serviceTypeRepository.ServiceTypeExists(serviceTypeId))
                return NotFound();

            var map = _mapper.Map<ServiceTypeDto>(_serviceTypeRepository.GetServiceType(serviceTypeId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(map);
        }

        //Create
        [HttpPost]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        public IActionResult CreateServiceType([FromBody] ServiceTypeDto create)
        {
            // id tự tăng nên không cần check Exitsts (suy nghỉ cá nhân thoi).
            if (create == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var map = _mapper.Map<ServiceType>(create);

            if (!_serviceTypeRepository.CreateServiceType(map))
            {
                ModelState.AddModelError("", "Something went wrong while create");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        // Update
        [HttpPut("{serviceTypeId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult UpdateAction(int serviceTypeId, [FromBody] ServiceTypeDto updated)
        {
            if (updated == null)
                return BadRequest(ModelState);

            if (serviceTypeId != updated.Id)
                return BadRequest(ModelState);

            if (!_serviceTypeRepository.ServiceTypeExists(serviceTypeId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var map = _mapper.Map<ServiceType>(updated);

            if (!_serviceTypeRepository.UpdateServiceType(map))
            {
                ModelState.AddModelError("", "Something went wrong updating ServiceType");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Updated");
        }

        // Delete
        [HttpDelete("{serviceTypeId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult DeleteServiceType(int serviceTypeId)
        {
            if (!_serviceTypeRepository.ServiceTypeExists(serviceTypeId))
            {
                return NotFound();
            }

            var delete = _serviceTypeRepository.GetServiceType(serviceTypeId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_serviceTypeRepository.DeleteServiceType(delete))
            {
                ModelState.AddModelError("", "Something went wrong deleting ServiceType");
            }

            return Ok("Successfully Deleted");
        }
    }
}
