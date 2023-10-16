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
    public class PaymentStatusController : ControllerBase
    {
        private readonly IPaymentStatusRepository _paymentStatusRepository;
        private readonly IMapper _mapper;

        public PaymentStatusController(IPaymentStatusRepository paymentStatusRepository, IMapper mapper)
        {
            _paymentStatusRepository = paymentStatusRepository;
            _mapper = mapper;
        }

        // Get ALL 
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PaymentStatus>))]
        public IActionResult GetAllPaymentStatus()
        {
            var paymentStatuss = _mapper.Map<List<PaymentStatusDto>>(_paymentStatusRepository.GetAllPaymentStatus());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(paymentStatuss);
        }

        // Get By ID
        [HttpGet("{paymentStatusId}")]
        [ProducesResponseType(200, Type = typeof(PaymentStatus))]
        //[ProducesResponseType(400)]
        public IActionResult GetPaymentStatus(int paymentStatusId)
        {
            if (!_paymentStatusRepository.PaymentStatusExists(paymentStatusId))
                return NotFound();

            var map = _mapper.Map<PaymentStatusDto>(_paymentStatusRepository.GetPaymentStatus(paymentStatusId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(map);
        }

        //Create
        [HttpPost]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        public IActionResult CreatePaymentStatus([FromBody] PaymentStatusDto create)
        {
            // id tự tăng nên không cần check Exitsts (suy nghỉ cá nhân thoi).
            if (create == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var map = _mapper.Map<PaymentStatus>(create);

            if (!_paymentStatusRepository.CreatePaymentStatus(map))
            {
                ModelState.AddModelError("", "Something went wrong while create");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        // Update
        [HttpPut("{paymentStatusId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult UpdateAction(int paymentStatusId, [FromBody] PaymentStatusDto updated)
        {
            if (updated == null)
                return BadRequest(ModelState);

            if (paymentStatusId != updated.Id)
                return BadRequest(ModelState);

            if (!_paymentStatusRepository.PaymentStatusExists(paymentStatusId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var map = _mapper.Map<PaymentStatus>(updated);

            if (!_paymentStatusRepository.UpdatePaymentStatus(map))
            {
                ModelState.AddModelError("", "Something went wrong updating PaymentStatus");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Updated");
        }

        // Delete
        [HttpDelete("{paymentStatusId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult DeletePaymentStatus(int paymentStatusId)
        {
            if (!_paymentStatusRepository.PaymentStatusExists(paymentStatusId))
            {
                return NotFound();
            }

            var delete = _paymentStatusRepository.GetPaymentStatus(paymentStatusId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_paymentStatusRepository.DeletePaymentStatus(delete))
            {
                ModelState.AddModelError("", "Something went wrong deleting PaymentStatus");
            }

            return Ok("Successfully Deleted");
        }
    }
}
