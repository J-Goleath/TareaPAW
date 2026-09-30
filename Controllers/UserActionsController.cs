using Microsoft.AspNetCore.Mvc;
using PAW.Models.DTO;
using PAW.Repositories;

namespace TareaPAW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserActionsController : ControllerBase
    {
        private readonly IUserActionRepository _repository;

        public UserActionsController(IUserActionRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserActionDTO>>> GetUserActions()
        {
            var items = await _repository.ReadAsync();
            return Ok(items.Select(UserActionDTO.ConvertFrom));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserActionDTO>> GetUserAction(int id)
        {
            var entity = await _repository.FindAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            return Ok(UserActionDTO.ConvertFrom(entity));
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAction(UserActionDTO dto)
        {
            var entity = UserActionDTO.ConvertTo(dto);
            var result = await _repository.CreateAsync(entity);

            if (!result)
            {
                return BadRequest();
            }

            return Ok(UserActionDTO.ConvertFrom(entity));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserAction(int id, UserActionDTO dto)
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

            return Ok(UserActionDTO.ConvertFrom(entity));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAction(int id)
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
