using AutoMapper;
using FinalProject.Core.Models;
using FinalProject.Core.Repositories.Contract;
using FinalProject.Core.Specifictions;
using FinalProject.Core.Specifictions.ProductSpecifiction;
using FinalProjectApi.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectApi.Controllers
{

    public class ProductController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IMapper _mapper;

        public ProductController(IGenericRepository<Product> productRepo , IMapper mapper) 
        {
            _productRepo = productRepo;
           _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProudcts()
        {
            var spec = new ProductIncludes();
            var products = await _productRepo.GetAllWithSpecAsync(spec);

            return Ok(_mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products));
        }

        [HttpGet("id")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var spec = new ProductIncludes(id);
            var product =  await _productRepo.GetWithSpecAsync(spec);
            if (product == null){
                return NotFound(new { Message = "Not Found" });
            }
            return Ok(_mapper.Map<Product , ProductDto>(product));
        }

    }


}
