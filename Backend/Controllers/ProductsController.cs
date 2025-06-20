using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Formats.Asn1;
using VagueVault.Backend.DTOs.Products;
using VagueVault.Backend.Services.Product;

namespace VagueVault.Backend.Controllers
{
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
            var Products = await _productsService.GetProductsAsync();
            return Ok(new { Products });
        }


        [HttpGet("By_Id")]
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



        [HttpGet("By_Category")]
        public async Task<IActionResult> GetByCategory(int id)
        {
            var Category = await _productsService.GetProductByCategoriesId(id);
            return Ok(new { Category });
        }




        [Authorize(Policy = "AdminOnly")]
        [HttpPost("Add_Product")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Add([FromForm] ProductDto product)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var data = await _productsService.CreateProductAsync(product);
            return CreatedAtAction(nameof(Add), data);

        }


        [Authorize(Policy = "AdminOnly")]
        [HttpPut("update_product")]
        public async Task<IActionResult> Update( int id,[FromForm] ProductDto product)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var Response = await _productsService.UpdateProductAsync(id, product);
            return Ok(new { Response });
        }




        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("Soft_Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var DeletedStatus = await _productsService.DeleteProductAsync(id);
            return Ok(new { DeletedStatus });
        }



        [Authorize(Policy = "AdminOnly")]
        [HttpPost("Add_Category")]

        public async Task<IActionResult> CreateCategory(int id, string name)
        {
          var Category = await  _productsService.AddCategory(id, name);
            return Ok(new {Category});
        }



        [Authorize(Policy = "AdminOnly")]
        [HttpPost("Add_Status")]

        public async Task<IActionResult> CreateStatus(int id, string name)
        {
            var Status = await _productsService.AddStatus(id, name);
            return Ok(new {Status});
        }



        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("Delete_category")]

        public async Task<IActionResult> DeleteCategory(int id)
        {
            var DeletedStatus = await _productsService.DeleteCategory(id);
            return Ok(new { DeletedStatus });
        }


        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("Delete_Status")]

        public async Task<IActionResult> DeleteStatus(int id)
        {
            var DeletedStatus = await _productsService.DeleteStatus(id);
            return Ok(new { DeletedStatus });
        }
    }
}

