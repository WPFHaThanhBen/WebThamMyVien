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
    public class ShoppingCartItemController : ControllerBase
    {
        private readonly IShoppingCartItemRepository _shoppingCartItemRepository;
        private readonly IMapper _mapper;

        public ShoppingCartItemController(IShoppingCartItemRepository shoppingCartItemRepository, IMapper mapper)
        {
            _shoppingCartItemRepository = shoppingCartItemRepository;
            _mapper = mapper;
        }

        // Get ALL 
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ShoppingCartItem>))]
        public IActionResult GetAllShoppingCartItem()
        {
            var shoppingCartItems = _mapper.Map<List<ShoppingCartItemDto>>(_shoppingCartItemRepository.GetAllShoppingCartItem());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(shoppingCartItems);
        }
        // 
        //Scaffold-DbContext “Data Source=DESKTOP-125DL48\SQLEXPRESS;Initial Catalog=QL_CosmeticClinic_V2;Integrated Security=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -force
        [HttpGet("ByShoppingCartId/{id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ShoppingCartItem>))]
        public IActionResult GetAllShoppingCartItemByShoppingCartId(int id)
        {
            var shoppingCartItems = _mapper.Map<List<ShoppingCartItemDto>>(_shoppingCartItemRepository.GetAllShoppingCartItemByShoppingCartId(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(shoppingCartItems);
        }

        // Get By ID
        [HttpGet("{shoppingCartItemId}")]
        [ProducesResponseType(200, Type = typeof(ShoppingCartItem))]
        //[ProducesResponseType(400)]
        public IActionResult GetShoppingCartItem(int shoppingCartItemId)
        {
            if (!_shoppingCartItemRepository.ShoppingCartItemExists(shoppingCartItemId))
                return NotFound();

            var map = _mapper.Map<ShoppingCartItemDto>(_shoppingCartItemRepository.GetShoppingCartItem(shoppingCartItemId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(map);
        }

        //Create
        [HttpPost]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        public IActionResult CreateShoppingCartItem([FromBody] ShoppingCartItemDto create)
        {
            // id tự tăng nên không cần check Exitsts (suy nghỉ cá nhân thoi).
            if (create == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var map = _mapper.Map<ShoppingCartItem>(create);

            if (!_shoppingCartItemRepository.CreateShoppingCartItem(map))
            {
                ModelState.AddModelError("", "Something went wrong while create");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        // Update
        [HttpPut("{shoppingCartItemId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult UpdateAction(int shoppingCartItemId, [FromBody] ShoppingCartItemDto updated)
        {
            if (updated == null)
                return BadRequest(ModelState);

            if (shoppingCartItemId != updated.Id)
                return BadRequest(ModelState);

            if (!_shoppingCartItemRepository.ShoppingCartItemExists(shoppingCartItemId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var map = _mapper.Map<ShoppingCartItem>(updated);

            if (!_shoppingCartItemRepository.UpdateShoppingCartItem(map))
            {
                ModelState.AddModelError("", "Something went wrong updating ShoppingCartItem");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Updated");
        }

        // Delete
        [HttpDelete("{shoppingCartItemId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult DeleteShoppingCartItem(int shoppingCartItemId)
        {
            if (!_shoppingCartItemRepository.ShoppingCartItemExists(shoppingCartItemId))
            {
                return NotFound();
            }

            var delete = _shoppingCartItemRepository.GetShoppingCartItem(shoppingCartItemId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_shoppingCartItemRepository.DeleteShoppingCartItem(delete))
            {
                ModelState.AddModelError("", "Something went wrong deleting ShoppingCartItem");
            }

            return Ok("Successfully Deleted");
        }
    }
}
