using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductRepository repo) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(string? brand, string? type, string? sort)
        {
            return Ok(await repo.GetProductsAsync(brand,type,sort));
        }

        [HttpGet("{id:int}")] //api/products/number
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var productId = await repo.GetProductByIdAsync(id);
            if (productId == null) return NotFound();
            return productId;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            repo.AddProduct(product);
            if (await repo.SaveAsyncChanges())
            {
                return CreatedAtAction("GetProduct", new { id = product }, product);
            }
            return BadRequest("Cannot update this product");

            
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Product>> UpdateProduct(int id, Product product)
        {
            if (product.Id != id || !(ProductExists(id))) return BadRequest("cannot find any product with this id");


            repo.UpdateProduct(product);

            if (await repo.SaveAsyncChanges())
            {
                return NoContent();
            }
            return BadRequest("Problem updating the product");
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var productId = await repo.GetProductByIdAsync(id);
            if (productId == null) return NotFound();
            repo.DeleteProduct(productId);
            if (await repo.SaveAsyncChanges())
            {
                return NoContent();
            }
            return BadRequest("Problem deleting the product");
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
        {
            return Ok(await repo.GetBrandsAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
        {
            return Ok(await repo.GetTypesAsync());
        }

        [HttpGet("masala")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetMasala() {
            return Ok(await repo.GetMasalaAsync());
        }

        private bool ProductExists(int id)
        {
            return repo.ProductExists(id);
        }
    }
}
