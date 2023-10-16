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
    public class ProductBranchController : ControllerBase
    {
        private readonly IProductBranchRepository _productBranchRepository;
        private readonly IMapper _mapper;

        public ProductBranchController(IProductBranchRepository productBranchRepository, IMapper mapper)
        {
            _productBranchRepository = productBranchRepository;
            _mapper = mapper;
        }

        // Get ALL 
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ProductBranch>))]
        public IActionResult GetAllProductBranch()
        {
            var productBranchs = _mapper.Map<List<ProductBranchDto>>(_productBranchRepository.GetAllProductBranch());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(productBranchs);
        }

        // Get By ID
        [HttpGet("{productBranchId}")]
        [ProducesResponseType(200, Type = typeof(ProductBranch))]
        //[ProducesResponseType(400)]
        public IActionResult GetProductBranch(int productBranchId)
        {
            if (!_productBranchRepository.ProductBranchExists(productBranchId))
                return NotFound();

            var map = _mapper.Map<ProductBranchDto>(_productBranchRepository.GetProductBranch(productBranchId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(map);
        }

        //Create
        [HttpPost]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        public IActionResult CreateProductBranch([FromBody] ProductBranchDto create)
        {
            // id tự tăng nên không cần check Exitsts (suy nghỉ cá nhân thoi).
            if (create == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var map = _mapper.Map<ProductBranch>(create);

            if (!_productBranchRepository.CreateProductBranch(map))
            {
                ModelState.AddModelError("", "Something went wrong while create");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        // Update
        [HttpPut("{productBranchId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult UpdateAction(int productBranchId, [FromBody] ProductBranchDto updated)
        {
            if (updated == null)
                return BadRequest(ModelState);

            if (productBranchId != updated.Id)
                return BadRequest(ModelState);

            if (!_productBranchRepository.ProductBranchExists(productBranchId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var map = _mapper.Map<ProductBranch>(updated);

            if (!_productBranchRepository.UpdateProductBranch(map))
            {
                ModelState.AddModelError("", "Something went wrong updating ProductBranch");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Updated");
        }

        // Delete
        [HttpDelete("{productBranchId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult DeleteProductBranch(int productBranchId)
        {
            if (!_productBranchRepository.ProductBranchExists(productBranchId))
            {
                return NotFound();
            }

            var delete = _productBranchRepository.GetProductBranch(productBranchId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_productBranchRepository.DeleteProductBranch(delete))
            {
                ModelState.AddModelError("", "Something went wrong deleting ProductBranch");
            }

            return Ok("Successfully Deleted");
        }
    }
}
