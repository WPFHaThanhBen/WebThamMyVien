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
    public class InvoiceTypeController : ControllerBase
    {
        private readonly IInvoiceTypeRepository _invoiceTypeRepository;
        private readonly IMapper _mapper;

        public InvoiceTypeController(IInvoiceTypeRepository invoiceTypeRepository, IMapper mapper)
        {
            _invoiceTypeRepository = invoiceTypeRepository;
            _mapper = mapper;
        }

        // Get ALL 
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<InvoiceType>))]
        public IActionResult GetAllInvoiceType()
        {
            var invoiceTypes = _mapper.Map<List<InvoiceTypeDto>>(_invoiceTypeRepository.GetAllInvoiceType());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(invoiceTypes);
        }

        // Get By ID
        [HttpGet("{invoiceTypeIdepository}")]
        [ProducesResponseType(200, Type = typeof(InvoiceType))]
        //[ProducesResponseType(400)]
        public IActionResult GetInvoiceType(int invoiceTypeIdepository)
        {
            if (!_invoiceTypeRepository.InvoiceTypeExists(invoiceTypeIdepository))
                return NotFound();

            var map = _mapper.Map<InvoiceTypeDto>(_invoiceTypeRepository.GetInvoiceType(invoiceTypeIdepository));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(map);
        }

        //Create
        [HttpPost]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        public IActionResult CreateInvoiceType([FromBody] InvoiceTypeDto create)
        {
            // id tự tăng nên không cần check Exitsts (suy nghỉ cá nhân thoi).
            if (create == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var map = _mapper.Map<InvoiceType>(create);

            if (!_invoiceTypeRepository.CreateInvoiceType(map))
            {
                ModelState.AddModelError("", "Something went wrong while create");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        // Update
        [HttpPut("{invoiceTypeIdepository}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult UpdateAction(int invoiceTypeIdepository, [FromBody] InvoiceTypeDto updated)
        {
            if (updated == null)
                return BadRequest(ModelState);

            if (invoiceTypeIdepository != updated.Id)
                return BadRequest(ModelState);

            if (!_invoiceTypeRepository.InvoiceTypeExists(invoiceTypeIdepository))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var map = _mapper.Map<InvoiceType>(updated);

            if (!_invoiceTypeRepository.UpdateInvoiceType(map))
            {
                ModelState.AddModelError("", "Something went wrong updating InvoiceType");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Updated");
        }

        // Delete
        [HttpDelete("{invoiceTypeIdepository}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult DeleteInvoiceType(int invoiceTypeIdepository)
        {
            if (!_invoiceTypeRepository.InvoiceTypeExists(invoiceTypeIdepository))
            {
                return NotFound();
            }

            var delete = _invoiceTypeRepository.GetInvoiceType(invoiceTypeIdepository);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_invoiceTypeRepository.DeleteInvoiceType(delete))
            {
                ModelState.AddModelError("", "Something went wrong deleting InvoiceType");
            }

            return Ok("Successfully Deleted");
        }
    }
}
