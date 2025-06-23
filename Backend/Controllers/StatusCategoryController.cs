using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VagueVault.Backend.Services.Statuses;

namespace VagueVault.Backend.Controllers
{
    [Authorize(Policy = "AdminOnly")]

    [Route("api/StatusCategory")]
    [ApiController]
    public class StatusCategoryController : ControllerBase
    {
        private readonly ICategoryStatusServices _categoryStatusServices;
        public StatusCategoryController(ICategoryStatusServices categoryStatusServices)
        {
            _categoryStatusServices = categoryStatusServices;
        }

        [HttpGet("viewCategories")]
        public async Task<IActionResult> GetCategory()
        {
            var Categories = await _categoryStatusServices.ViewCategories();
           return Ok(new {Categories});   
        }
        [HttpGet("viewStatus")]
        public async Task<IActionResult> GetStatus()
        {
            var Status = await _categoryStatusServices.ViewStatus();
            return Ok(new { Status });
        }

        [HttpPost("addCategory")]

        public async Task<IActionResult> CreateCategory( string name)
        {
            var Category = await _categoryStatusServices.AddCategory(name);
            return Ok(new { Category });
        }



        [HttpPost("addStatus")]

        public async Task<IActionResult> CreateStatus(string name)
        { 
            var Status = await _categoryStatusServices.AddStatus(name);
            return Ok(new { Status });
        }



        [HttpDelete("deleteCategory/{id}")]

        public async Task<IActionResult> DeleteCategory(int id)
        {
            var DeletedStatus = await _categoryStatusServices.DeleteCategory(id);
            return Ok(new { DeletedStatus });
        }


        [HttpDelete("deleteStatus/{id}")]

        public async Task<IActionResult> DeleteStatus(int id)
        {
            var DeletedStatus = await _categoryStatusServices.DeleteStatus(id);
            return Ok(new { DeletedStatus });
        }
    }
}
