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
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMapper _mapper;

        public InvoiceController(IInvoiceRepository invoiceRepository, IMapper mapper)
        {
            _invoiceRepository = invoiceRepository;
            _mapper = mapper;
        }

        // Get ALL 
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Invoice>))]
        public IActionResult GetAllInvoice()
        {
            var invoices = _mapper.Map<List<InvoiceDto>>(_invoiceRepository.GetAllInvoice());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(invoices);
        }
		// Get ALL 
		[HttpGet("InvoiceByType/{invoiceTypeId}")]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Invoice>))]
		public IActionResult GetAllInvoice(int invoiceTypeId)
		{
			var invoices = _mapper.Map<List<InvoiceDto>>(_invoiceRepository.GetAllInvoiceByType(invoiceTypeId));

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(invoices);
		}
		// Get By ID
		[HttpGet("{invoiceId}")]
        [ProducesResponseType(200, Type = typeof(Invoice))]
        //[ProducesResponseType(400)]
        public IActionResult GetInvoice(int invoiceId)
        {
            if (!_invoiceRepository.InvoiceExists(invoiceId))
                return NotFound();

            var map = _mapper.Map<InvoiceDto>(_invoiceRepository.GetInvoice(invoiceId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(map);
        }

        //Create
        [HttpPost]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        public IActionResult CreateInvoice([FromBody] InvoiceDto create)
        {
            // id tự tăng nên không cần check Exitsts (suy nghỉ cá nhân thoi).
            if (create == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var map = _mapper.Map<Invoice>(create);

            if (!_invoiceRepository.CreateInvoice(map))
            {
                ModelState.AddModelError("", "Something went wrong while create");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        // Update
        [HttpPut("{invoiceId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult UpdateAction(int invoiceId, [FromBody] InvoiceDto updated)
        {
            if (updated == null)
                return BadRequest(ModelState);

            if (invoiceId != updated.Id)
                return BadRequest(ModelState);

            if (!_invoiceRepository.InvoiceExists(invoiceId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var map = _mapper.Map<Invoice>(updated);

            if (!_invoiceRepository.UpdateInvoice(map))
            {
                ModelState.AddModelError("", "Something went wrong updating Invoice");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Updated");
        }

        // Delete
        [HttpDelete("{invoiceId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult DeleteInvoice(int invoiceId)
        {
            if (!_invoiceRepository.InvoiceExists(invoiceId))
            {
                return NotFound();
            }

            var delete = _invoiceRepository.GetInvoice(invoiceId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_invoiceRepository.DeleteInvoice(delete))
            {
                ModelState.AddModelError("", "Something went wrong deleting Invoice");
            }

            return Ok("Successfully Deleted");
        }
    }
}
