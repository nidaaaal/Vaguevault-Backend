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



        [HttpGet("By_Category")]
        public async Task<IActionResult> GetByCategory(int id)
        {
            var data = await _productsService.GetProductByCategoriesId(id);
            return data == null ? NotFound(data) : Ok(data);
        }




        [Authorize(Policy = "AdminOnly")]
        [HttpPost("Add_Product")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Add([FromForm] ProductDto product)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var data = await _productsService.CreateProductAsync(product);
            if (data == null)
                return Conflict("A product with the same name already exists.");
            return CreatedAtAction(nameof(Add), data);

        }



        [Authorize(Policy = "AdminOnly")]
        [HttpPut("update_product")]
        public async Task<IActionResult> Update( int id,[FromForm] ProductDto product)
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



        [Authorize(Policy = "AdminOnly")]
        [HttpPost("Add_Category")]

        public async Task<IActionResult> CreateCategory(int id, string name)
        {
          var data = await  _productsService.AddCategory(id, name);
            return Ok(data);
        }



        [Authorize(Policy = "AdminOnly")]
        [HttpPost("Add_Status")]

        public async Task<IActionResult> CreateStatus(int id, string name)
        {
            var data = await _productsService.AddStatus(id, name);
            return Ok(data);
        }



        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("Delete_category")]

        public async Task<IActionResult> DeleteCategory(int id)
        {
            var data = await _productsService.DeleteCategory(id);
            return data ? NoContent() : NotFound();
        }


        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("Delete_Status")]

        public async Task<IActionResult> DeleteStatus(int id)
        {
            var data = await _productsService.DeleteStatus(id);
            return data ? NoContent() : NotFound();
        }


    }
}

