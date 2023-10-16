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
    public class AppointmentStatusController : ControllerBase
    {
        private readonly IAppointmentStatusRepository _appointmentStatusRepository;
        private readonly IMapper _mapper;

        public AppointmentStatusController(IAppointmentStatusRepository appointmentStatusRepository, IMapper mapper)
        {
            _appointmentStatusRepository = appointmentStatusRepository;
            _mapper = mapper;
        }

        // Get ALL 
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AppointmentStatus>))]
        public IActionResult GetAllAppointmentStatus()
        {
            var appointmentStatuss = _mapper.Map<List<AppointmentStatusDto>>(_appointmentStatusRepository.GetAllAppointmentStatus());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(appointmentStatuss);
        }

        // Get By ID
        [HttpGet("{appointmentStatusId}")]
        [ProducesResponseType(200, Type = typeof(AppointmentStatus))]
        //[ProducesResponseType(400)]
        public IActionResult GetAppointmentStatus(int appointmentStatusId)
        {
            if (!_appointmentStatusRepository.AppointmentStatusExists(appointmentStatusId))
                return NotFound();

            var map = _mapper.Map<AppointmentStatusDto>(_appointmentStatusRepository.GetAppointmentStatus(appointmentStatusId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(map);
        }

        //Create
        [HttpPost]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        public IActionResult CreateAppointmentStatus([FromBody] AppointmentStatusDto create)
        {
            // id tự tăng nên không cần check Exitsts (suy nghỉ cá nhân thoi).
            if (create == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var map = _mapper.Map<AppointmentStatus>(create);

            if (!_appointmentStatusRepository.CreateAppointmentStatus(map))
            {
                ModelState.AddModelError("", "Something went wrong while create");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        // Update
        [HttpPut("{appointmentStatusId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult UpdateAction(int appointmentStatusId, [FromBody] AppointmentStatusDto updated)
        {
            if (updated == null)
                return BadRequest(ModelState);

            if (appointmentStatusId != updated.Id)
                return BadRequest(ModelState);

            if (!_appointmentStatusRepository.AppointmentStatusExists(appointmentStatusId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var map = _mapper.Map<AppointmentStatus>(updated);

            if (!_appointmentStatusRepository.UpdateAppointmentStatus(map))
            {
                ModelState.AddModelError("", "Something went wrong updating AppointmentStatus");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Updated");
        }

        // Delete
        [HttpDelete("{appointmentStatusId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult DeleteAppointmentStatus(int appointmentStatusId)
        {
            if (!_appointmentStatusRepository.AppointmentStatusExists(appointmentStatusId))
            {
                return NotFound();
            }

            var delete = _appointmentStatusRepository.GetAppointmentStatus(appointmentStatusId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_appointmentStatusRepository.DeleteAppointmentStatus(delete))
            {
                ModelState.AddModelError("", "Something went wrong deleting AppointmentStatus");
            }

            return Ok("Successfully Deleted");
        }
    }
}
