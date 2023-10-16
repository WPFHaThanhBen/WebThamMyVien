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
    public class OnlinePaymentController : ControllerBase
    {
        private readonly IOnlinePaymentRepository _onlinePaymentRepository;
        private readonly IMapper _mapper;

        public OnlinePaymentController(IOnlinePaymentRepository onlinePaymentRepository, IMapper mapper)
        {
            _onlinePaymentRepository = onlinePaymentRepository;
            _mapper = mapper;
        }

        // Get ALL 
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<OnlinePayment>))]
        public IActionResult GetAllOnlinePayment()
        {
            var onlinePayments = _mapper.Map<List<OnlinePaymentDto>>(_onlinePaymentRepository.GetAllOnlinePayment());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(onlinePayments);
        }

        // Get By ID
        [HttpGet("{onlinePaymentId}")]
        [ProducesResponseType(200, Type = typeof(OnlinePayment))]
        //[ProducesResponseType(400)]
        public IActionResult GetOnlinePayment(int onlinePaymentId)
        {
            if (!_onlinePaymentRepository.OnlinePaymentExists(onlinePaymentId))
                return NotFound();

            var map = _mapper.Map<OnlinePaymentDto>(_onlinePaymentRepository.GetOnlinePayment(onlinePaymentId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(map);
        }

        //Create
        [HttpPost]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        public IActionResult CreateOnlinePayment([FromBody] OnlinePaymentDto create)
        {
            // id tự tăng nên không cần check Exitsts (suy nghỉ cá nhân thoi).
            if (create == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var map = _mapper.Map<OnlinePayment>(create);

            if (!_onlinePaymentRepository.CreateOnlinePayment(map))
            {
                ModelState.AddModelError("", "Something went wrong while create");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        // Update
        [HttpPut("{onlinePaymentId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult UpdateAction(int onlinePaymentId, [FromBody] OnlinePaymentDto updated)
        {
            if (updated == null)
                return BadRequest(ModelState);

            if (onlinePaymentId != updated.Id)
                return BadRequest(ModelState);

            if (!_onlinePaymentRepository.OnlinePaymentExists(onlinePaymentId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var map = _mapper.Map<OnlinePayment>(updated);

            if (!_onlinePaymentRepository.UpdateOnlinePayment(map))
            {
                ModelState.AddModelError("", "Something went wrong updating OnlinePayment");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Updated");
        }

        // Delete
        [HttpDelete("{onlinePaymentId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult DeleteOnlinePayment(int onlinePaymentId)
        {
            if (!_onlinePaymentRepository.OnlinePaymentExists(onlinePaymentId))
            {
                return NotFound();
            }

            var delete = _onlinePaymentRepository.GetOnlinePayment(onlinePaymentId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_onlinePaymentRepository.DeleteOnlinePayment(delete))
            {
                ModelState.AddModelError("", "Something went wrong deleting OnlinePayment");
            }

            return Ok("Successfully Deleted");
        }
    }
}
