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
    public class BranchController : ControllerBase
    {
        private readonly IBranchRepository _branchRepository;
        private readonly IMapper _mapper;

        public BranchController(IBranchRepository branchRepository, IMapper mapper)
        {
            _branchRepository = branchRepository;
            _mapper = mapper;
        }

        // Get ALL 
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Branch>))]
        public IActionResult GetAllBranch()
        {
            var branchs = _mapper.Map<List<BranchDto>>(_branchRepository.GetAllBranch());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(branchs);
        }

        // Get By ID
        [HttpGet("{branchId}")]
        [ProducesResponseType(200, Type = typeof(Branch))]
        //[ProducesResponseType(400)]
        public IActionResult GetBranch(int branchId)
        {
            if (!_branchRepository.BranchExists(branchId))
                return NotFound();

            var map = _mapper.Map<BranchDto>(_branchRepository.GetBranch(branchId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(map);
        }

        //Create
        [HttpPost]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        public IActionResult CreateBranch([FromBody] BranchDto create)
        {
            // id tự tăng nên không cần check Exitsts (suy nghỉ cá nhân thoi).
            if (create == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var map = _mapper.Map<Branch>(create);

            if (!_branchRepository.CreateBranch(map))
            {
                ModelState.AddModelError("", "Something went wrong while create");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        // Update
        [HttpPut("{branchId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult UpdateAction(int branchId, [FromBody] BranchDto updated)
        {
            if (updated == null)
                return BadRequest(ModelState);

            if (branchId != updated.Id)
                return BadRequest(ModelState);

            if (!_branchRepository.BranchExists(branchId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var map = _mapper.Map<Branch>(updated);

            if (!_branchRepository.UpdateBranch(map))
            {
                ModelState.AddModelError("", "Something went wrong updating Branch");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Updated");
        }

        // Delete
        [HttpDelete("{branchId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        public IActionResult DeleteBranch(int branchId)
        {
            if (!_branchRepository.BranchExists(branchId))
            {
                return NotFound();
            }

            var delete = _branchRepository.GetBranch(branchId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_branchRepository.DeleteBranch(delete))
            {
                ModelState.AddModelError("", "Something went wrong deleting Branch");
            }

            return Ok("Successfully Deleted");
        }
    }
}
