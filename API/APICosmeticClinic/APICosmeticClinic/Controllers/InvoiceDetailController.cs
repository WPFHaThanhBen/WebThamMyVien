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
    public class InvoiceDetailController : ControllerBase
    {
        private readonly IInvoiceDetailRepository _invoiceDetailRepository;
        private readonly IMapper _mapper;

        public InvoiceDetailController(IInvoiceDetailRepository invoiceDetailRepository, IMapper mapper)
        {
            _invoiceDetailRepository = invoiceDetailRepository;
            _mapper = mapper;
        }

        // Get ALL 
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<InvoiceDetail>))]
        public IActionResult GetAllInvoiceDetail()
        {
            var invoiceDetails = _mapper.Map<List<InvoiceDetailDto>>(_invoiceDetailRepository.GetAllInvoiceDetail());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(invoiceDetails);
        }
		// Get ALL 
		[HttpGet("InvoiceDetailByInvoice/{invoiceId}")]
		[ProducesResponseType(200, Type = typeof(IEnumerable<InvoiceDetail>))]
		public IActionResult GetAllInvoiceDetail(int invoiceId)
		{
			var invoiceDetails = _mapper.Map<List<InvoiceDetailDto>>(_invoiceDetailRepository.GetInvoiceDetailByInvoice(invoiceId));

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(invoiceDetails);
		}
		// Get By ID
		[HttpGet("{invoiceDetailId}")]
        [ProducesResponseType(200, Type = typeof(InvoiceDetail))]
        //[ProducesResponseType(400)]
        public IActionResult GetInvoiceDetail(int invoiceDetailId)
        {
            if (!_invoiceDetailRepository.InvoiceDetailExists(invoiceDetailId))
                return NotFound();

            var map = _mapper.Map<InvoiceDetailDto>(_invoiceDetailRepository.GetInvoiceDetail(invoiceDetailId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(map);
        }

        //Create
        [HttpPost]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        public IActionResult CreateInvoiceDetail([FromBody] InvoiceDetailDto create)
        {
            // id tự tăng nên không cần check Exitsts (suy nghỉ cá nhân thoi).
            if (create == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var map = _mapper.Map<InvoiceDetail>(create);

            if (!_invoiceDetailRepository.CreateInvoiceDetail(map))
            {
                ModelState.AddModelError("", "Something went wrong while create");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        // Update
        [HttpPut("{invoiceDetailId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult UpdateAction(int invoiceDetailId, [FromBody] InvoiceDetailDto updated)
        {
            if (updated == null)
                return BadRequest(ModelState);

            if (invoiceDetailId != updated.Id)
                return BadRequest(ModelState);

            if (!_invoiceDetailRepository.InvoiceDetailExists(invoiceDetailId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var map = _mapper.Map<InvoiceDetail>(updated);

            if (!_invoiceDetailRepository.UpdateInvoiceDetail(map))
            {
                ModelState.AddModelError("", "Something went wrong updating InvoiceDetail");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Updated");
        }

        // Delete
        [HttpDelete("{invoiceDetailId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult DeleteInvoiceDetail(int invoiceDetailId)
        {
            if (!_invoiceDetailRepository.InvoiceDetailExists(invoiceDetailId))
            {
                return NotFound();
            }

            var delete = _invoiceDetailRepository.GetInvoiceDetail(invoiceDetailId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_invoiceDetailRepository.DeleteInvoiceDetail(delete))
            {
                ModelState.AddModelError("", "Something went wrong deleting InvoiceDetail");
            }

            return Ok("Successfully Deleted");
        }
    }
}
