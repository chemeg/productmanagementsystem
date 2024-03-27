using Microsoft.AspNetCore.Mvc;
using ProductManagementSystem.Models;
using ProductManagementSystem.Services;

namespace ProductManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var products = await _productService.GetProductsAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving products.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving the product.");
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchProducts([FromQuery] string name)
        {
            try
            {
                var products = await _productService.GetProductsByNameAsync(name);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while searching for products.");
            }
        }

        [HttpGet("total-count")]
        public async Task<IActionResult> GetTotalProductCount()
        {
            try
            {
                var totalCount = await _productService.GetTotalProductCountAsync();
                return Ok(totalCount);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving the total product count.");
            }
        }

        [HttpGet("category/{category}")]
        public async Task<IActionResult> GetProductsByCategory(string category)
        {
            try
            {
                var products = await _productService.GetProductsByCategoryAsync(category);
                if (products.Count == 0)
                {
                    return NotFound($"No products found for category '{category}'.");
                }
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving products by category.");
            }
        }

        [HttpGet("sort")]
        public async Task<IActionResult> GetSortedProducts([FromQuery] string sortBy, [FromQuery] string sortOrder = "asc")
        {
            try
            {
                var products = await _productService.GetSortedProductsAsync(sortBy.ToLower(), sortOrder.ToLower());
                return Ok(products);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving sorted products.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            try
            {
                await _productService.UpdateProductAsync(id, product);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the product.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] ProductDto productDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var product = MapToProductEntity(productDto);

                await _productService.AddProductAsync(product);

                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while adding the product.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await _productService.DeleteProductAsync(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the product.");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAllProducts()
        {
            try
            {
                await _productService.DeleteAllProductsAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting all products.");
            }
        }

        private Product MapToProductEntity(ProductDto productDto)
        {
            return new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Category = productDto.Category,
                Price = productDto.Price
            };
        }
    }
}
