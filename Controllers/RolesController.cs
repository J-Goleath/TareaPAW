using Microsoft.AspNetCore.Mvc;
using PAW.Models.DTO;
using PAW.Repositories;

namespace TareaPAW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleRepository _repository;

        public RolesController(IRoleRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDTO>>> GetRoles()
        {
            var items = await _repository.ReadAsync();
            return Ok(items.Select(RoleDTO.ConvertFrom));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoleDTO>> GetRole(int id)
        {
            var entity = await _repository.FindAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            return Ok(RoleDTO.ConvertFrom(entity));
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleDTO dto)
        {
            var entity = RoleDTO.ConvertTo(dto);
            var result = await _repository.CreateAsync(entity);

            if (!result)
            {
                return BadRequest();
            }

            return Ok(RoleDTO.ConvertFrom(entity));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(int id, RoleDTO dto)
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

            return Ok(RoleDTO.ConvertFrom(entity));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
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
