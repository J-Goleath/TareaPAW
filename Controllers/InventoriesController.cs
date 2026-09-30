using Microsoft.AspNetCore.Mvc;
using PAW.Models.DTO;
using PAW.Repositories;

namespace TareaPAW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoriesController : ControllerBase
    {
        private readonly IInventoryRepository _repository;

        public InventoriesController(IInventoryRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryDTO>>> GetInventories()
        {
            var items = await _repository.ReadAsync();
            return Ok(items.Select(InventoryDTO.ConvertFrom));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryDTO>> GetInventory(int id)
        {
            var entity = await _repository.FindAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            return Ok(InventoryDTO.ConvertFrom(entity));
        }

        [HttpPost]
        public async Task<IActionResult> CreateInventory(InventoryDTO dto)
        {
            var entity = InventoryDTO.ConvertTo(dto);
            var result = await _repository.CreateAsync(entity);

            if (!result)
            {
                return BadRequest();
            }

            return Ok(InventoryDTO.ConvertFrom(entity));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInventory(int id, InventoryDTO dto)
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

            return Ok(InventoryDTO.ConvertFrom(entity));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventory(int id)
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
