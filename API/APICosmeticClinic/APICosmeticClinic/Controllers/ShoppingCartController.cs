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
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IMapper _mapper;

        public ShoppingCartController(IShoppingCartRepository shoppingCartRepository, IMapper mapper)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _mapper = mapper;
        }

        // Get ALL 
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ShoppingCart>))]
        public IActionResult GetAllShoppingCart()
        {
            var shoppingCarts = _mapper.Map<List<ShoppingCartDto>>(_shoppingCartRepository.GetAllShoppingCart());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(shoppingCarts);
        }

        // Get By ID
        [HttpGet("{shoppingCartId}")]
        [ProducesResponseType(200, Type = typeof(ShoppingCart))]
        //[ProducesResponseType(400)]
        public IActionResult GetShoppingCart(int shoppingCartId)
        {
            if (!_shoppingCartRepository.ShoppingCartExists(shoppingCartId))
                return NotFound();

            var map = _mapper.Map<ShoppingCartDto>(_shoppingCartRepository.GetShoppingCart(shoppingCartId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(map);
        }

        //Create
        [HttpPost]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        public IActionResult CreateShoppingCart([FromBody] ShoppingCartDto create)
        {
            // id tự tăng nên không cần check Exitsts (suy nghỉ cá nhân thoi).
            if (create == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var map = _mapper.Map<ShoppingCart>(create);

            if (!_shoppingCartRepository.CreateShoppingCart(map))
            {
                ModelState.AddModelError("", "Something went wrong while create");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        // Update
        [HttpPut("{shoppingCartId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult UpdateAction(int shoppingCartId, [FromBody] ShoppingCartDto updated)
        {
            if (updated == null)
                return BadRequest(ModelState);

            if (shoppingCartId != updated.Id)
                return BadRequest(ModelState);

            if (!_shoppingCartRepository.ShoppingCartExists(shoppingCartId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var map = _mapper.Map<ShoppingCart>(updated);

            if (!_shoppingCartRepository.UpdateShoppingCart(map))
            {
                ModelState.AddModelError("", "Something went wrong updating ShoppingCart");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Updated");
        }

        // Delete
        [HttpDelete("{shoppingCartId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult DeleteShoppingCart(int shoppingCartId)
        {
            if (!_shoppingCartRepository.ShoppingCartExists(shoppingCartId))
            {
                return NotFound();
            }

            var delete = _shoppingCartRepository.GetShoppingCart(shoppingCartId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_shoppingCartRepository.DeleteShoppingCart(delete))
            {
                ModelState.AddModelError("", "Something went wrong deleting ShoppingCart");
            }

            return Ok("Successfully Deleted");
        }
    }
}
