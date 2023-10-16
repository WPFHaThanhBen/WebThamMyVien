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
    public class ProductTypeController : ControllerBase
    {
        private readonly IProductTypeRepository _productTypeRepository;
        private readonly IMapper _mapper;

        public ProductTypeController(IProductTypeRepository productTypeRepository, IMapper mapper)
        {
            _productTypeRepository = productTypeRepository;
            _mapper = mapper;
        }

        // Get ALL 
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ProductType>))]
        public IActionResult GetAllProductType()
        {
            var productTypes = _mapper.Map<List<ProductTypeDto>>(_productTypeRepository.GetAllProductType());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(productTypes);
        }

        // Get By ID
        [HttpGet("{productTypeId}")]
        [ProducesResponseType(200, Type = typeof(ProductType))]
        //[ProducesResponseType(400)]
        public IActionResult GetProductType(int productTypeId)
        {
            if (!_productTypeRepository.ProductTypeExists(productTypeId))
                return NotFound();

            var map = _mapper.Map<ProductTypeDto>(_productTypeRepository.GetProductType(productTypeId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(map);
        }

        //Create
        [HttpPost]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        public IActionResult CreateProductType([FromBody] ProductTypeDto create)
        {
            // id tự tăng nên không cần check Exitsts (suy nghỉ cá nhân thoi).
            if (create == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var map = _mapper.Map<ProductType>(create);

            if (!_productTypeRepository.CreateProductType(map))
            {
                ModelState.AddModelError("", "Something went wrong while create");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        // Update
        [HttpPut("{productTypeId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult UpdateAction(int productTypeId, [FromBody] ProductTypeDto updated)
        {
            if (updated == null)
                return BadRequest(ModelState);

            if (productTypeId != updated.Id)
                return BadRequest(ModelState);

            if (!_productTypeRepository.ProductTypeExists(productTypeId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var map = _mapper.Map<ProductType>(updated);

            if (!_productTypeRepository.UpdateProductType(map))
            {
                ModelState.AddModelError("", "Something went wrong updating ProductType");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Updated");
        }

        // Delete
        [HttpDelete("{productTypeId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult DeleteProductType(int productTypeId)
        {
            if (!_productTypeRepository.ProductTypeExists(productTypeId))
            {
                return NotFound();
            }

            var delete = _productTypeRepository.GetProductType(productTypeId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_productTypeRepository.DeleteProductType(delete))
            {
                ModelState.AddModelError("", "Something went wrong deleting ProductType");
            }

            return Ok("Successfully Deleted");
        }
    }
}
