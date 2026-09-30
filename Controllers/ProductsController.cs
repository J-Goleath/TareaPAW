using Microsoft.AspNetCore.Mvc;
using PAW.Models.DTO;
using PAW.Repositories;

namespace TareaPAW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;

        public ProductsController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            var items = await _repository.ReadAsync();
            return Ok(items.Select(ProductDTO.ConvertFrom));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            var entity = await _repository.FindAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            return Ok(ProductDTO.ConvertFrom(entity));
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductDTO dto)
        {
            var entity = ProductDTO.ConvertTo(dto);
            var result = await _repository.CreateAsync(entity);

            if (!result)
            {
                return BadRequest();
            }

            return Ok(ProductDTO.ConvertFrom(entity));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductDTO dto)
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

            return Ok(ProductDTO.ConvertFrom(entity));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
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
