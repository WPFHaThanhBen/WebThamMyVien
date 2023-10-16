using APICosmeticClinic.Dto;
using APICosmeticClinic.Interfaces;
using APICosmeticClinic.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace APICosmeticClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActionController : ControllerBase
    {
        private readonly IActionRepository _actionRepository;
        private readonly IMapper _mapper;

        public ActionController(IActionRepository actionRepository, IMapper mapper)
        {
            _actionRepository = actionRepository;
            _mapper = mapper;
        }

        // Get ALL 
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Models.Action>))]
        public IActionResult GetAllAction()
        {
            var actions = _mapper.Map<List<ActionDto>>(_actionRepository.GetAllAction());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(actions);
        }

        // Get By ID
        [HttpGet("{actionId}")]
        [ProducesResponseType(200, Type = typeof(Models.Action))]
        //[ProducesResponseType(400)]
        public IActionResult GetAction(int actionId)
        {
            if (!_actionRepository.ActionExists(actionId))
                return NotFound();

            var map = _mapper.Map<ActionDto>(_actionRepository.GetAction(actionId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(map);
        }

        //Create
        [HttpPost]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        public IActionResult CreateAction([FromBody] ActionDto create)
        {
            // id tự tăng nên không cần check Exitsts (suy nghỉ cá nhân thoi).
            if (create == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var map = _mapper.Map<Models.Action>(create);

            if (!_actionRepository.CreateAction(map))
            {
                ModelState.AddModelError("", "Something went wrong while create");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        // Update
        [HttpPut("{actionId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult UpdateAction(int actionId, [FromBody] ActionDto updated)
        {
            if (updated == null)
                return BadRequest(ModelState);

            if (actionId != updated.Id)
                return BadRequest(ModelState);

            if (!_actionRepository.ActionExists(actionId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var map = _mapper.Map<Models.Action>(updated);

            if (!_actionRepository.UpdateAction(map))
            {
                ModelState.AddModelError("", "Something went wrong updating Action");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Updated");
        }

        // Delete
        [HttpDelete("{actionId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult DeleteAction(int actionId)
        {
            if (!_actionRepository.ActionExists(actionId))
            {
                return NotFound();
            }

            var delete = _actionRepository.GetAction(actionId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_actionRepository.DeleteAction(delete))
            {
                ModelState.AddModelError("", "Something went wrong deleting Action");
            }

            return Ok("Successfully Deleted");
        }
    }
}
