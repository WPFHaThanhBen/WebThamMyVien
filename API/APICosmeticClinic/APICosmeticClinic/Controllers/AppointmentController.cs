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
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;

        public AppointmentController(IAppointmentRepository appointmentRepository, IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
        }

        // Get ALL 
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Appointment>))]
        public IActionResult GetAllAppointment()
        {
            var appointments = _mapper.Map<List<AppointmentDto>>(_appointmentRepository.GetAllAppointment());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(appointments);
        }

        // Get By ID
        [HttpGet("{appointmentId}")]
        [ProducesResponseType(200, Type = typeof(Appointment))]
        //[ProducesResponseType(400)]
        public IActionResult GetAppointment(int appointmentId)
        {
            if (!_appointmentRepository.AppointmentExists(appointmentId))
                return NotFound();

            var map = _mapper.Map<AppointmentDto>(_appointmentRepository.GetAppointment(appointmentId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(map);
        }

        //Create
        [HttpPost]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        public IActionResult CreateAppointment([FromBody] AppointmentDto create)
        {
            // id tự tăng nên không cần check Exitsts (suy nghỉ cá nhân thoi).
            if (create == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var map = _mapper.Map<Appointment>(create);

            if (!_appointmentRepository.CreateAppointment(map))
            {
                ModelState.AddModelError("", "Something went wrong while create");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        // Update
        [HttpPut("{appointmentId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult UpdateAction(int appointmentId, [FromBody] AppointmentDto updated)
        {
            if (updated == null)
                return BadRequest(ModelState);

            if (appointmentId != updated.Id)
                return BadRequest(ModelState);

            if (!_appointmentRepository.AppointmentExists(appointmentId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var map = _mapper.Map<Appointment>(updated);

            if (!_appointmentRepository.UpdateAppointment(map))
            {
                ModelState.AddModelError("", "Something went wrong updating Appointment");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Updated");
        }

        // Delete
        [HttpDelete("{appointmentId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult DeleteAppointment(int appointmentId)
        {
            if (!_appointmentRepository.AppointmentExists(appointmentId))
            {
                return NotFound();
            }

            var delete = _appointmentRepository.GetAppointment(appointmentId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_appointmentRepository.DeleteAppointment(delete))
            {
                ModelState.AddModelError("", "Something went wrong deleting Appointment");
            }

            return Ok("Successfully Deleted");
        }
    }
}
