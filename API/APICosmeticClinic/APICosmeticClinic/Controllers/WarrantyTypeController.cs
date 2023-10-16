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
    public class WarrantyTypeController : ControllerBase
    {
        private readonly IWarrantyTypeRepository _warrantyTypeRepository;
        private readonly IMapper _mapper;

        public WarrantyTypeController(IWarrantyTypeRepository warrantyTypeRepository, IMapper mapper)
        {
            _warrantyTypeRepository = warrantyTypeRepository;
            _mapper = mapper;
        }

        // Get ALL 
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<WarrantyType>))]
        public IActionResult GetAllWarrantyType()
        {
            var warrantyTypes = _mapper.Map<List<WarrantyTypeDto>>(_warrantyTypeRepository.GetAllWarrantyType());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(warrantyTypes);
        }

        // Get By ID
        [HttpGet("{warrantyTypeId}")]
        [ProducesResponseType(200, Type = typeof(WarrantyType))]
        //[ProducesResponseType(400)]
        public IActionResult GetWarrantyType(int warrantyTypeId)
        {
            if (!_warrantyTypeRepository.WarrantyTypeExists(warrantyTypeId))
                return NotFound();

            var map = _mapper.Map<WarrantyTypeDto>(_warrantyTypeRepository.GetWarrantyType(warrantyTypeId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(map);
        }

        //Create
        [HttpPost]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        public IActionResult CreateWarrantyType([FromBody] WarrantyTypeDto create)
        {
            // id tự tăng nên không cần check Exitsts (suy nghỉ cá nhân thoi).
            if (create == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var map = _mapper.Map<WarrantyType>(create);

            if (!_warrantyTypeRepository.CreateWarrantyType(map))
            {
                ModelState.AddModelError("", "Something went wrong while create");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        // Update
        [HttpPut("{warrantyTypeId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult UpdateAction(int warrantyTypeId, [FromBody] WarrantyTypeDto updated)
        {
            if (updated == null)
                return BadRequest(ModelState);

            if (warrantyTypeId != updated.Id)
                return BadRequest(ModelState);

            if (!_warrantyTypeRepository.WarrantyTypeExists(warrantyTypeId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var map = _mapper.Map<WarrantyType>(updated);

            if (!_warrantyTypeRepository.UpdateWarrantyType(map))
            {
                ModelState.AddModelError("", "Something went wrong updating WarrantyType");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Updated");
        }

        // Delete
        [HttpDelete("{warrantyTypeId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult DeleteWarrantyType(int warrantyTypeId)
        {
            if (!_warrantyTypeRepository.WarrantyTypeExists(warrantyTypeId))
            {
                return NotFound();
            }

            var delete = _warrantyTypeRepository.GetWarrantyType(warrantyTypeId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_warrantyTypeRepository.DeleteWarrantyType(delete))
            {
                ModelState.AddModelError("", "Something went wrong deleting WarrantyType");
            }

            return Ok("Successfully Deleted");
        }
    }
}
