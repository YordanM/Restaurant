using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Models;
using Restaurant.Business.Services;
using Restaurant.Domain.Models;
using System.Threading.Tasks;

namespace Restaurant.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(
            [FromQuery] PaggingParameters paggingParameters,
            string? product = null,
            string? categoryId = null,
            string? sortBy = null,
            SortDirection? sortDirection = SortDirection.Ascending
            )
        {
            var products = await _productService.GetAllAsync(
                paggingParameters,
                sortDirection,
                product,
                categoryId,
                sortBy);

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] string id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> AddProductAsync([FromBody] ProductRequest request)
        {
            var product = request.ToProductModel();

            await _productService.AddProductAsync(product);

            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductAsync(
            [FromBody] ProductRequest request,
            [FromRoute] string id)
        {
            var result = await _productService.UpdateProductAsync(id, request.ToProductModel());

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductAsync([FromRoute] string id)
        {
            await _productService.DeleteProductAsync(id);

            return Ok();
        }
    }
}
