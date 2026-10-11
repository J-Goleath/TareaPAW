using PAW.Models.DTO;

namespace PAW.Web.Models;

public class ProductViewModel
{
    public IEnumerable<ProductDTO> Products { get; set; } = new List<ProductDTO>();
}
