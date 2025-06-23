using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Formats.Asn1;
using VagueVault.Backend.DTOs.Products;
using VagueVault.Backend.Services.Product;

namespace VagueVault.Backend.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductServices _productsService;

        public ProductsController(IProductServices productServices)
        {
            _productsService = productServices;
        }

        
        [HttpGet("View")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> Get()
        {
            var Products = await _productsService.GetProductsAsync();
            return Ok(new { Products });
        }


        [HttpGet("View/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
           var product = await _productsService.GetByIdAsync(id);
            return Ok(new { product });
        }



        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] string search)
        {
            var Response = await _productsService.GetProductBySearch(search);
            return Ok(new { Response });
        }



        [HttpGet("Category")]
        public async Task<IActionResult> GetByCategory(int id)
        {
            var Category = await _productsService.GetProductByCategoriesId(id);
            return Ok(new { Category });
        }




        [Authorize(Policy = "AdminOnly")]
        [HttpPost("create")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Add([FromForm] ProductAddDto product)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var data = await _productsService.CreateProductAsync(product);
            return CreatedAtAction(nameof(Add), data);

        }        


        [Authorize(Policy = "AdminOnly")]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update( int id,[FromForm] int price)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var Response = await _productsService.UpdateProductPriceAsync(id, price);
            return Ok(new { Response });
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPut("UpdatePrice/{id}")]
        public async Task<IActionResult> UpdatePrice(int id, [FromForm] ProductAddDto product)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var Response = await _productsService.UpdateProductAsync(id, product);
            return Ok(new { Response });
        }





        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("softDelete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var DeletedStatus = await _productsService.DeleteProductAsync(id);
            return Ok(new { DeletedStatus });
        }

    }
}

