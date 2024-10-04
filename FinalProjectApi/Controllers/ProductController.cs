using FinalProject.Core.Models;
using FinalProject.Core.Repositories.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectApi.Controllers
{

    public class ProductController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepo;

        public ProductController(IGenericRepository<Product> productRepo) 
        {
            _productRepo = productRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProudcts()
        {
            var products = await  _productRepo.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("id")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
         var product =  await _productRepo.GetAsync(id);
            if (product == null){
                return NotFound(new { Message = "Not Found" });
            }
            return Ok(product);
        }

    }


}
