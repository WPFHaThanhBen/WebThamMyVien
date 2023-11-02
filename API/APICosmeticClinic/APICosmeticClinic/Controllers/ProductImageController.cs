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
    public class ProductImageController : ControllerBase
    {
        private readonly IProductImageRepository _productImageRepository;
        private readonly IMapper _mapper;

        public ProductImageController(IProductImageRepository productImageRepository, IMapper mapper)
        {
            _productImageRepository = productImageRepository;
            _mapper = mapper;
        }

        // Get ALL 
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ProductImage>))]
        public IActionResult GetAllProductImage()
        {
            var productImages = _mapper.Map<List<ProductImageDto>>(_productImageRepository.GetAllProductImage());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(productImages);
        }
		// Get ALL 
		[HttpGet("ProductImageByProduct/{productId}")]
		[ProducesResponseType(200, Type = typeof(IEnumerable<ProductImage>))]
		public IActionResult GetAllProductImage(int productId)
		{
			var productImages = _mapper.Map<List<ProductImageDto>>(_productImageRepository.GetAllProductImageByProduct(productId));

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(productImages);
		}

		// Get By ID
		[HttpGet("{productImageId}")]
        [ProducesResponseType(200, Type = typeof(ProductImage))]
        //[ProducesResponseType(400)]
        public IActionResult GetProductImage(int productImageId)
        {
            if (!_productImageRepository.ProductImageExists(productImageId))
                return NotFound();

            var map = _mapper.Map<ProductImageDto>(_productImageRepository.GetProductImage(productImageId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(map);
        }

        //Create
        [HttpPost]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        public IActionResult CreateProductImage([FromBody] ProductImageDto create)
        {
            // id tự tăng nên không cần check Exitsts (suy nghỉ cá nhân thoi).
            if (create == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var map = _mapper.Map<ProductImage>(create);

            if (!_productImageRepository.CreateProductImage(map))
            {
                ModelState.AddModelError("", "Something went wrong while create");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        // Update
        [HttpPut("{productImageId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult UpdateAction(int productImageId, [FromBody] ProductImageDto updated)
        {
            if (updated == null)
                return BadRequest(ModelState);

            if (productImageId != updated.Id)
                return BadRequest(ModelState);

            if (!_productImageRepository.ProductImageExists(productImageId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var map = _mapper.Map<ProductImage>(updated);

            if (!_productImageRepository.UpdateProductImage(map))
            {
                ModelState.AddModelError("", "Something went wrong updating ProductImage");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Updated");
        }

        // Delete
        [HttpDelete("{productImageId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult DeleteProductImage(int productImageId)
        {
            if (!_productImageRepository.ProductImageExists(productImageId))
            {
                return NotFound();
            }

            var delete = _productImageRepository.GetProductImage(productImageId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_productImageRepository.DeleteProductImage(delete))
            {
                ModelState.AddModelError("", "Something went wrong deleting ProductImage");
            }

            return Ok("Successfully Deleted");
        }
    }
}
