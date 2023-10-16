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
    public class OrderStatusController : ControllerBase
    {
        private readonly IOrderStatusRepository _orderStatusRepository;
        private readonly IMapper _mapper;

        public OrderStatusController(IOrderStatusRepository orderStatusRepository, IMapper mapper)
        {
            _orderStatusRepository = orderStatusRepository;
            _mapper = mapper;
        }

        // Get ALL 
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<OrderStatus>))]
        public IActionResult GetAllOrderStatus()
        {
            var orderStatuss = _mapper.Map<List<OrderStatusDto>>(_orderStatusRepository.GetAllOrderStatus());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(orderStatuss);
        }

        // Get By ID
        [HttpGet("{orderStatusId}")]
        [ProducesResponseType(200, Type = typeof(OrderStatus))]
        //[ProducesResponseType(400)]
        public IActionResult GetOrderStatus(int orderStatusId)
        {
            if (!_orderStatusRepository.OrderStatusExists(orderStatusId))
                return NotFound();

            var map = _mapper.Map<OrderStatusDto>(_orderStatusRepository.GetOrderStatus(orderStatusId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(map);
        }

        //Create
        [HttpPost]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        public IActionResult CreateOrderStatus([FromBody] OrderStatusDto create)
        {
            // id tự tăng nên không cần check Exitsts (suy nghỉ cá nhân thoi).
            if (create == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var map = _mapper.Map<OrderStatus>(create);

            if (!_orderStatusRepository.CreateOrderStatus(map))
            {
                ModelState.AddModelError("", "Something went wrong while create");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        // Update
        [HttpPut("{orderStatusId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult UpdateAction(int orderStatusId, [FromBody] OrderStatusDto updated)
        {
            if (updated == null)
                return BadRequest(ModelState);

            if (orderStatusId != updated.Id)
                return BadRequest(ModelState);

            if (!_orderStatusRepository.OrderStatusExists(orderStatusId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var map = _mapper.Map<OrderStatus>(updated);

            if (!_orderStatusRepository.UpdateOrderStatus(map))
            {
                ModelState.AddModelError("", "Something went wrong updating OrderStatus");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Updated");
        }

        // Delete
        [HttpDelete("{orderStatusId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult DeleteOrderStatus(int orderStatusId)
        {
            if (!_orderStatusRepository.OrderStatusExists(orderStatusId))
            {
                return NotFound();
            }

            var delete = _orderStatusRepository.GetOrderStatus(orderStatusId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_orderStatusRepository.DeleteOrderStatus(delete))
            {
                ModelState.AddModelError("", "Something went wrong deleting OrderStatus");
            }

            return Ok("Successfully Deleted");
        }
    }
}
