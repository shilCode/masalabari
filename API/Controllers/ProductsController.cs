using API.RequestHelpers;
using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{

    public class ProductsController(IGenericRepo<Product> repo) : BaseApiController
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery] ProductSpecParam specParam)
        {
            var spec = new ProductFilterSpec(specParam);
            
            return await CreatePagedResult(repo,spec,specParam.PageIndex,specParam.PageSize);
        }

        [HttpGet("{id:int}")] //api/products/number
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var productId = await repo.GetByIdAsync(id);
            if (productId == null) return NotFound();
            return productId;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            repo.Add(product);
            if (await repo.SaveAllAsync())
            {
                return CreatedAtAction("GetProduct", new { id = product }, product);
            }
            return BadRequest("Cannot update this product");

            
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Product>> UpdateProduct(int id, Product product)
        {
            if (product.Id != id || !(ProductExists(id))) return BadRequest("cannot find any product with this id");


            repo.Update(product);

            if (await repo.SaveAllAsync())
            {
                return NoContent();
            }
            return BadRequest("Problem updating the product");
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var productId = await repo.GetByIdAsync(id);
            if (productId == null) return NotFound();
            repo.Remove(productId);
            if (await repo.SaveAllAsync())
            {
                return NoContent();
            }
            return BadRequest("Problem deleting the product");
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
        {
            var spec = new BrandSpec();

            return Ok(await repo.ListAsync(spec));
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
        {
            var spec = new TypeListSpec();
            return Ok(await repo.ListAsync(spec));
        }

        [HttpGet("masala")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetMasala() {
            // TODO: Implement method
            return Ok();
        }

        private bool ProductExists(int id)
        {
            return repo.Exist(id);
        }
    }
}
