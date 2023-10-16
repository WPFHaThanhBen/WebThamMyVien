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
    public class PromotionController : ControllerBase
    {
        private readonly IPromotionRepository _promotionRepository;
        private readonly IMapper _mapper;

        public PromotionController(IPromotionRepository promotionRepository, IMapper mapper)
        {
            _promotionRepository = promotionRepository;
            _mapper = mapper;
        }

        // Get ALL 
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Promotion>))]
        public IActionResult GetAllPromotion()
        {
            var promotions = _mapper.Map<List<PromotionDto>>(_promotionRepository.GetAllPromotion());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(promotions);
        }

        // Get By ID
        [HttpGet("{promotionId}")]
        [ProducesResponseType(200, Type = typeof(Promotion))]
        //[ProducesResponseType(400)]
        public IActionResult GetPromotion(int promotionId)
        {
            if (!_promotionRepository.PromotionExists(promotionId))
                return NotFound();

            var map = _mapper.Map<PromotionDto>(_promotionRepository.GetPromotion(promotionId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(map);
        }

        //Create
        [HttpPost]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        public IActionResult CreatePromotion([FromBody] PromotionDto create)
        {
            // id tự tăng nên không cần check Exitsts (suy nghỉ cá nhân thoi).
            if (create == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var map = _mapper.Map<Promotion>(create);

            if (!_promotionRepository.CreatePromotion(map))
            {
                ModelState.AddModelError("", "Something went wrong while create");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        // Update
        [HttpPut("{promotionId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult UpdateAction(int promotionId, [FromBody] PromotionDto updated)
        {
            if (updated == null)
                return BadRequest(ModelState);

            if (promotionId != updated.Id)
                return BadRequest(ModelState);

            if (!_promotionRepository.PromotionExists(promotionId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var map = _mapper.Map<Promotion>(updated);

            if (!_promotionRepository.UpdatePromotion(map))
            {
                ModelState.AddModelError("", "Something went wrong updating Promotion");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Updated");
        }

        // Delete
        [HttpDelete("{promotionId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult DeletePromotion(int promotionId)
        {
            if (!_promotionRepository.PromotionExists(promotionId))
            {
                return NotFound();
            }

            var delete = _promotionRepository.GetPromotion(promotionId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_promotionRepository.DeletePromotion(delete))
            {
                ModelState.AddModelError("", "Something went wrong deleting Promotion");
            }

            return Ok("Successfully Deleted");
        }
    }
}
