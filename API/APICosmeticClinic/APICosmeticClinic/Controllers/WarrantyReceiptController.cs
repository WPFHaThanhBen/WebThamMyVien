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
    public class WarrantyReceiptController : ControllerBase
    {
        private readonly IWarrantyReceiptRepository _warrantyReceiptRepository;
        private readonly IMapper _mapper;

        public WarrantyReceiptController(IWarrantyReceiptRepository warrantyReceiptRepository, IMapper mapper)
        {
            _warrantyReceiptRepository = warrantyReceiptRepository;
            _mapper = mapper;
        }

        // Get ALL 
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<WarrantyReceipt>))]
        public IActionResult GetAllWarrantyReceipt()
        {
            var warrantyReceipts = _mapper.Map<List<WarrantyReceiptDto>>(_warrantyReceiptRepository.GetAllWarrantyReceipt());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(warrantyReceipts);
        }

        // Get By ID
        [HttpGet("{warrantyReceiptId}")]
        [ProducesResponseType(200, Type = typeof(WarrantyReceipt))]
        //[ProducesResponseType(400)]
        public IActionResult GetWarrantyReceipt(int warrantyReceiptId)
        {
            if (!_warrantyReceiptRepository.WarrantyReceiptExists(warrantyReceiptId))
                return NotFound();

            var map = _mapper.Map<WarrantyReceiptDto>(_warrantyReceiptRepository.GetWarrantyReceipt(warrantyReceiptId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(map);
        }

        //Create
        [HttpPost]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        public IActionResult CreateWarrantyReceipt([FromBody] WarrantyReceiptDto create)
        {
            // id tự tăng nên không cần check Exitsts (suy nghỉ cá nhân thoi).
            if (create == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var map = _mapper.Map<WarrantyReceipt>(create);

            if (!_warrantyReceiptRepository.CreateWarrantyReceipt(map))
            {
                ModelState.AddModelError("", "Something went wrong while create");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        // Update
        [HttpPut("{warrantyReceiptId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult UpdateAction(int warrantyReceiptId, [FromBody] WarrantyReceiptDto updated)
        {
            if (updated == null)
                return BadRequest(ModelState);

            if (warrantyReceiptId != updated.Id)
                return BadRequest(ModelState);

            if (!_warrantyReceiptRepository.WarrantyReceiptExists(warrantyReceiptId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var map = _mapper.Map<WarrantyReceipt>(updated);

            if (!_warrantyReceiptRepository.UpdateWarrantyReceipt(map))
            {
                ModelState.AddModelError("", "Something went wrong updating WarrantyReceipt");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Updated");
        }

        // Delete
        [HttpDelete("{warrantyReceiptId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult DeleteWarrantyReceipt(int warrantyReceiptId)
        {
            if (!_warrantyReceiptRepository.WarrantyReceiptExists(warrantyReceiptId))
            {
                return NotFound();
            }

            var delete = _warrantyReceiptRepository.GetWarrantyReceipt(warrantyReceiptId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_warrantyReceiptRepository.DeleteWarrantyReceipt(delete))
            {
                ModelState.AddModelError("", "Something went wrong deleting WarrantyReceipt");
            }

            return Ok("Successfully Deleted");
        }
    }
}
