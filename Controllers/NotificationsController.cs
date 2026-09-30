using Microsoft.AspNetCore.Mvc;
using PAW.Models.DTO;
using PAW.Repositories;

namespace TareaPAW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationRepository _repository;

        public NotificationsController(INotificationRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotificationDTO>>> GetNotifications()
        {
            var items = await _repository.ReadAsync();
            return Ok(items.Select(NotificationDTO.ConvertFrom));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NotificationDTO>> GetNotification(int id)
        {
            var entity = await _repository.FindAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            return Ok(NotificationDTO.ConvertFrom(entity));
        }

        [HttpPost]
        public async Task<IActionResult> CreateNotification(NotificationDTO dto)
        {
            var entity = NotificationDTO.ConvertTo(dto);
            var result = await _repository.CreateAsync(entity);

            if (!result)
            {
                return BadRequest();
            }

            return Ok(NotificationDTO.ConvertFrom(entity));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNotification(int id, NotificationDTO dto)
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

            return Ok(NotificationDTO.ConvertFrom(entity));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification(int id)
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
