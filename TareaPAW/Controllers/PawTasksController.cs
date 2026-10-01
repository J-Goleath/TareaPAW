using Microsoft.AspNetCore.Mvc;
using PAW.Models.DTO;
using PAW.Repositories;

namespace TareaPAW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PawTasksController : ControllerBase
    {
        private readonly IPawTaskRepository _repository;

        public PawTasksController(IPawTaskRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PawTaskDTO>>> GetPawTasks()
        {
            var items = await _repository.ReadAsync();
            return Ok(items.Select(PawTaskDTO.ConvertFrom));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PawTaskDTO>> GetPawTask(int id)
        {
            var entity = await _repository.FindAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            return Ok(PawTaskDTO.ConvertFrom(entity));
        }

        [HttpPost]
        public async Task<IActionResult> CreatePawTask(PawTaskDTO dto)
        {
            var entity = PawTaskDTO.ConvertTo(dto);
            var result = await _repository.CreateAsync(entity);

            if (!result)
            {
                return BadRequest();
            }

            return Ok(PawTaskDTO.ConvertFrom(entity));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePawTask(int id, PawTaskDTO dto)
        {
            var entity = await _repository.FindAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            dto.ApplyTo(entity);
            var result = await _repository.UpdateAsync(entity);

            if (!result)
            {
                return BadRequest();
            }

            return Ok(PawTaskDTO.ConvertFrom(entity));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePawTask(int id)
        {
            var entity = await _repository.FindAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            var result = await _repository.DeleteAsync(entity);

            if (!result)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
