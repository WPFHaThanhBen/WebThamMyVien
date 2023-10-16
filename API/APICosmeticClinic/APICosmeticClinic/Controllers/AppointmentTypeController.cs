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
    public class AppointmentTypeController : ControllerBase
    {
        private readonly IAppointmentTypeRepository _appointmentTypeRepository;
        private readonly IMapper _mapper;

        public AppointmentTypeController(IAppointmentTypeRepository appointmentTypeRepository, IMapper mapper)
        {
            _appointmentTypeRepository = appointmentTypeRepository;
            _mapper = mapper;
        }

        // Get ALL 
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AppointmentType>))]
        public IActionResult GetAllAppointmentType()
        {
            var appointmentTypes = _mapper.Map<List<AppointmentTypeDto>>(_appointmentTypeRepository.GetAllAppointmentType());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(appointmentTypes);
        }

        // Get By ID
        [HttpGet("{appointmentTypeId}")]
        [ProducesResponseType(200, Type = typeof(AppointmentType))]
        //[ProducesResponseType(400)]
        public IActionResult GetAppointmentType(int appointmentTypeId)
        {
            if (!_appointmentTypeRepository.AppointmentTypeExists(appointmentTypeId))
                return NotFound();

            var map = _mapper.Map<AppointmentTypeDto>(_appointmentTypeRepository.GetAppointmentType(appointmentTypeId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(map);
        }

        //Create
        [HttpPost]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        public IActionResult CreateAppointmentType([FromBody] AppointmentTypeDto create)
        {
            // id tự tăng nên không cần check Exitsts (suy nghỉ cá nhân thoi).
            if (create == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var map = _mapper.Map<AppointmentType>(create);

            if (!_appointmentTypeRepository.CreateAppointmentType(map))
            {
                ModelState.AddModelError("", "Something went wrong while create");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        // Update
        [HttpPut("{appointmentTypeId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult UpdateAction(int appointmentTypeId, [FromBody] AppointmentTypeDto updated)
        {
            if (updated == null)
                return BadRequest(ModelState);

            if (appointmentTypeId != updated.Id)
                return BadRequest(ModelState);

            if (!_appointmentTypeRepository.AppointmentTypeExists(appointmentTypeId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var map = _mapper.Map<AppointmentType>(updated);

            if (!_appointmentTypeRepository.UpdateAppointmentType(map))
            {
                ModelState.AddModelError("", "Something went wrong updating AppointmentType");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Updated"); 
        }

        // Delete
        [HttpDelete("{appointmentTypeId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult DeleteAppointmentType(int appointmentTypeId)
        {
            if (!_appointmentTypeRepository.AppointmentTypeExists(appointmentTypeId))
            {
                return NotFound();
            }

            var delete = _appointmentTypeRepository.GetAppointmentType(appointmentTypeId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_appointmentTypeRepository.DeleteAppointmentType(delete))
            {
                ModelState.AddModelError("", "Something went wrong deleting AppointmentType");
            }

            return Ok("Successfully Deleted");
        }
    }
}
