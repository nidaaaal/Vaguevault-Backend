using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Formats.Asn1;
using VagueVault.Backend.DTOs.Products;
using VagueVault.Backend.Services.Product;

namespace VagueVault.Backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductServices _productsService;

        public ProductsController(IProductServices productServices)
        {
            _productsService = productServices;
        }

        
        [HttpGet("All-Products")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> Get()
        {
            var data = await _productsService.GetProductsAsync();
            return Ok(data);
        }

        [HttpGet("By_Id")]
        public async Task<IActionResult> GetById(int id)
        {
           var data = await _productsService.GetByIdAsync(id);
            if (data == null) return NotFound("No Product found!");
            return Ok(data);
        }
        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] string search)
        {
            var data = await _productsService.GetProductBySearch(search);
            return data == null ? NotFound():Ok(data);
        }


        [Authorize(Policy = "AdminOnly")]
        [HttpPost("Add_Product")]
        public async Task<IActionResult> Add([FromBody] ProductDto product)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var data = await _productsService.CreateProductAsync(product);
            
            return CreatedAtAction(nameof(Add), data);

        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost("add_variant")]
        public async Task<IActionResult> VariantUpdate(int id, [FromBody] ProductVariantDto productVariant)
        {
            var data = await _productsService.CreateProductVariant(id, productVariant);
            return data == null ? NotFound() : Ok(data);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost("update_product")]
        public async Task<IActionResult> Update( int id,[FromBody] ProductDto product)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var data = await _productsService.UpdateProductAsync(id, product);
            return data == null ? NotFound("No data found on corresponding Productid"):Ok(data);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("Soft_Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _productsService.DeleteProductAsync(id);
            return data ? NoContent() : NotFound("No data found on corresponding Productid");

        }
       


    }
}

