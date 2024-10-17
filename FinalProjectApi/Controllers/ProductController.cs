using AutoMapper;
using FinalProject.Core.Models;
using FinalProject.Core.Repositories.Contract;
using FinalProject.Core.Specifictions;
using FinalProject.Core.Specifictions.ProductSpecifiction;
using FinalProjectApi.Dtos;
using FinalProjectApi.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectApi.Controllers
{

    public class ProductController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _brandsRepo;
        private readonly IGenericRepository<ProductCategory> _categoryRepo;
        private readonly IMapper _mapper;

        public ProductController(IGenericRepository<Product> productRepo , IGenericRepository<ProductBrand> brandsRepo,IGenericRepository<ProductCategory>CategoryRepo,IMapper mapper) 
        {
            _productRepo = productRepo;
           _brandsRepo = brandsRepo;
           _categoryRepo = CategoryRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProudcts()
        {
            var spec = new ProductIncludes();
            var products = await _productRepo.GetAllWithSpecAsync(spec);

            return Ok(_mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products));
        }
        [ProducesResponseType(typeof(ProductDto),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        [HttpGet("id")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var spec = new ProductIncludes(id);

            var product =  await _productRepo.GetWithSpecAsync(spec);
            if (product == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(_mapper.Map<Product , ProductDto>(product));
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<ProductBrand>>> GetBrand()
        {
            var brands = await _brandsRepo.GetAllAsync();

            return Ok(brands);
        }

        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<ProductCategory>>> GetCategory()
        {
            var Categories = await _categoryRepo.GetAllAsync();

            return Ok(Categories);
        }
    }


}
