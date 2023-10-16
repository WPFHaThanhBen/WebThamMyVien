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
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        // Get ALL 
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]
        public IActionResult GetAllProduct()
        {
            var products = _mapper.Map<List<ProductDto>>(_productRepository.GetAllProduct());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(products);
        }

        // Get By ID
        [HttpGet("{productId}")]
        [ProducesResponseType(200, Type = typeof(Product))]
        //[ProducesResponseType(400)]
        public IActionResult GetProduct(int productId)
        {
            if (!_productRepository.ProductExists(productId))
                return NotFound();

            var map = _mapper.Map<ProductDto>(_productRepository.GetProduct(productId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(map);
        }

        //Create
        [HttpPost]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        public IActionResult CreateProduct([FromBody] ProductDto create)
        {
            // id tự tăng nên không cần check Exitsts (suy nghỉ cá nhân thoi).
            if (create == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var map = _mapper.Map<Product>(create);

            if (!_productRepository.CreateProduct(map))
            {
                ModelState.AddModelError("", "Something went wrong while create");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        // Update
        [HttpPut("{productId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult UpdateAction(int productId, [FromBody] ProductDto updated)
        {
            if (updated == null)
                return BadRequest(ModelState);

            if (productId != updated.Id)
                return BadRequest(ModelState);

            if (!_productRepository.ProductExists(productId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var map = _mapper.Map<Product>(updated);

            if (!_productRepository.UpdateProduct(map))
            {
                ModelState.AddModelError("", "Something went wrong updating Product");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Updated");
        }

        // Delete
        [HttpDelete("{productId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult DeleteProduct(int productId)
        {
            if (!_productRepository.ProductExists(productId))
            {
                return NotFound();
            }

            var delete = _productRepository.GetProduct(productId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_productRepository.DeleteProduct(delete))
            {
                ModelState.AddModelError("", "Something went wrong deleting Product");
            }

            return Ok("Successfully Deleted");
        }
    }
}
