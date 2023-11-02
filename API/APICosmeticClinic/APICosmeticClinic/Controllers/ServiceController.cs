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
    public class ServiceController : ControllerBase
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;

        public ServiceController(IServiceRepository serviceRepository, IMapper mapper)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
        }

        // Get ALL 
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Service>))]
        public IActionResult GetAllService()
        {
            var services = _mapper.Map<List<ServiceDto>>(_serviceRepository.GetAllService());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(services);
        }
		// Get ALL 
		[HttpGet("ServiceByType/{serviceTypeId}")]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Service>))]
		public IActionResult GetAllService(int serviceTypeId)
		{
			var services = _mapper.Map<List<ServiceDto>>(_serviceRepository.GetAllServiceByType(serviceTypeId));

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(services);
		}
		// Get By ID
		[HttpGet("{serviceId}")]
        [ProducesResponseType(200, Type = typeof(Service))]
        //[ProducesResponseType(400)]
        public IActionResult GetService(int serviceId)
        {
            if (!_serviceRepository.ServiceExists(serviceId))
                return NotFound();

            var map = _mapper.Map<ServiceDto>(_serviceRepository.GetService(serviceId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(map);
        }

        //Create
        [HttpPost]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        public IActionResult CreateService([FromBody] ServiceDto create)
        {
            // id tự tăng nên không cần check Exitsts (suy nghỉ cá nhân thoi).
            if (create == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var map = _mapper.Map<Service>(create);

            if (!_serviceRepository.CreateService(map))
            {
                ModelState.AddModelError("", "Something went wrong while create");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        // Update
        [HttpPut("{serviceId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult UpdateAction(int serviceId, [FromBody] ServiceDto updated)
        {
            if (updated == null)
                return BadRequest(ModelState);

            if (serviceId != updated.Id)
                return BadRequest(ModelState);

            if (!_serviceRepository.ServiceExists(serviceId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var map = _mapper.Map<Service>(updated);

            if (!_serviceRepository.UpdateService(map))
            {
                ModelState.AddModelError("", "Something went wrong updating Service");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Updated");
        }

        // Delete
        [HttpDelete("{serviceId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult DeleteService(int serviceId)
        {
            if (!_serviceRepository.ServiceExists(serviceId))
            {
                return NotFound();
            }

            var delete = _serviceRepository.GetService(serviceId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_serviceRepository.DeleteService(delete))
            {
                ModelState.AddModelError("", "Something went wrong deleting Service");
            }

            return Ok("Successfully Deleted");
        }
    }
}
