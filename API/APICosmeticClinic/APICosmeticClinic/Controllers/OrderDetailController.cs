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
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IMapper _mapper;

        public OrderDetailController(IOrderDetailRepository orderDetailRepository, IMapper mapper)
        {
            _orderDetailRepository = orderDetailRepository;
            _mapper = mapper;
        }

        // Get ALL 
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<OrderDetail>))]
        public IActionResult GetAllOrderDetail()
        {
            var orderDetails = _mapper.Map<List<OrderDetailDto>>(_orderDetailRepository.GetAllOrderDetail());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(orderDetails);
        }


        // Get ALL 
        [HttpGet("ByOrderId/{id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<OrderDetail>))]
        public IActionResult GetAllOrderDetailByOrderId(int id)
        {
            var orderDetails = _mapper.Map<List<OrderDetailDto>>(_orderDetailRepository.GetAllOrderDetailByOrderId(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(orderDetails);
        }

        // Get By ID
        [HttpGet("{orderDetailId}")]
        [ProducesResponseType(200, Type = typeof(OrderDetail))]
        //[ProducesResponseType(400)]
        public IActionResult GetOrderDetail(int orderDetailId)
        {
            if (!_orderDetailRepository.OrderDetailExists(orderDetailId))
                return NotFound();

            var map = _mapper.Map<OrderDetailDto>(_orderDetailRepository.GetOrderDetail(orderDetailId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(map);
        }

        //Create
        [HttpPost]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        public IActionResult CreateOrderDetail([FromBody] OrderDetailDto create)
        {
            // id tự tăng nên không cần check Exitsts (suy nghỉ cá nhân thoi).
            if (create == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var map = _mapper.Map<OrderDetail>(create);

            if (!_orderDetailRepository.CreateOrderDetail(map))
            {
                ModelState.AddModelError("", "Something went wrong while create");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        // Update
        [HttpPut("{orderDetailId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult UpdateAction(int orderDetailId, [FromBody] OrderDetailDto updated)
        {
            if (updated == null)
                return BadRequest(ModelState);

            if (orderDetailId != updated.Id)
                return BadRequest(ModelState);

            if (!_orderDetailRepository.OrderDetailExists(orderDetailId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var map = _mapper.Map<OrderDetail>(updated);

            if (!_orderDetailRepository.UpdateOrderDetail(map))
            {
                ModelState.AddModelError("", "Something went wrong updating OrderDetail");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Updated");
        }

        // Delete
        [HttpDelete("{orderDetailId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult DeleteOrderDetail(int orderDetailId)
        {
            if (!_orderDetailRepository.OrderDetailExists(orderDetailId))
            {
                return NotFound();
            }

            var delete = _orderDetailRepository.GetOrderDetail(orderDetailId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_orderDetailRepository.DeleteOrderDetail(delete))
            {
                ModelState.AddModelError("", "Something went wrong deleting OrderDetail");
            }

            return Ok("Successfully Deleted");
        }
    }
}
