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
    public class ActionTypeController : ControllerBase
    {
        private readonly IActionTypeRepository _actionTypeRepository;
        private readonly IMapper _mapper;

        public ActionTypeController(IActionTypeRepository actionTypeRepository, IMapper mapper)
        {
            _actionTypeRepository = actionTypeRepository;
            _mapper = mapper;
        }

        // Get ALL 
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ActionType>))]
        public IActionResult GetAllActionType()
        {
            var actionTypes = _mapper.Map<List<ActionTypeDto>>(_actionTypeRepository.GetAllActionType());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(actionTypes);
        }

        // Get By ID
        [HttpGet("{actionTypeId}")]
        [ProducesResponseType(200, Type = typeof(ActionType))]
        //[ProducesResponseType(400)]
        public IActionResult GetActionType(int actionTypeId)
        {
            if (!_actionTypeRepository.ActionTypeExists(actionTypeId))
                return NotFound();

            var map = _mapper.Map<ActionTypeDto>(_actionTypeRepository.GetActionType(actionTypeId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(map);
        }

        //Create
        [HttpPost]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        public IActionResult CreateActionType([FromBody] ActionTypeDto create)
        {
            // id tự tăng nên không cần check Exitsts (suy nghỉ cá nhân thoi).
            if (create == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var map = _mapper.Map<ActionType>(create);

            if (!_actionTypeRepository.CreateActionType(map))
            {
                ModelState.AddModelError("", "Something went wrong while create");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        // Update
        [HttpPut("{actionTypeId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult UpdateAction(int actionTypeId, [FromBody] ActionTypeDto updated)
        {
            if (updated == null)
                return BadRequest(ModelState);

            if (actionTypeId != updated.Id)
                return BadRequest(ModelState);

            if (!_actionTypeRepository.ActionTypeExists(actionTypeId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var map = _mapper.Map<ActionType>(updated);

            if (!_actionTypeRepository.UpdateActionType(map))
            {
                ModelState.AddModelError("", "Something went wrong updating actionType");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Updated");
        }

        // Delete
        [HttpDelete("{actionTypeId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult DeleteActionType(int actionTypeId)
        {
            if (!_actionTypeRepository.ActionTypeExists(actionTypeId))
            {
                return NotFound();
            }

            var delete = _actionTypeRepository.GetActionType(actionTypeId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_actionTypeRepository.DeleteActionType(delete))
            {
                ModelState.AddModelError("", "Something went wrong deleting ActionType");
            }

            return Ok("Successfully Deleted");
        }
    }
}
