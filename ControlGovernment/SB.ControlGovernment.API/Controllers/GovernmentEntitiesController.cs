using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SB.ControlGovernment.Application.DTOs;
using SB.ControlGovernment.Application.Interfaces;

namespace SB.ControlGovernment.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class GovernmentEntitiesController : ControllerBase
    {
        private readonly IGovernmentEntityService _service;

        public GovernmentEntitiesController(IGovernmentEntityService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<GovernmentEntityDto>> GetAll()
        {
            var entities = _service.GetAll();
            return Ok(entities);
        }

        [HttpGet("{id}")]
        public ActionResult<GovernmentEntityDto> GetById(int id)
        {
            var entity = _service.GetById(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpPost]
        public ActionResult<GovernmentEntityDto> Add(GovernmentEntityDto dto)
        {
            _service.Add(dto);
            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, GovernmentEntityDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }
            _service.Update(dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return NoContent();
        }
    }
}
