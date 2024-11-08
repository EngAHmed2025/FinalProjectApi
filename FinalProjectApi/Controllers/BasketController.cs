using AutoMapper;
using FinalProject.Core.Models;
using FinalProject.Core.Repositories.Contract;
using FinalProjectApi.Dtos;
using FinalProjectApi.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository , IMapper mapper)
        {
           _basketRepository = basketRepository;
             _mapper = mapper;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string id )
        {
            var basket = await _basketRepository.GetBasketAsync(id);
            return Ok(basket ?? new CustomerBasket(id));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket) 
        {
            var mapperBasket = _mapper.Map<CustomerBasketDto, CustomerBasket>(basket);
            var createdorUpdatedBasket = await _basketRepository.UpdateBasketAsync(mapperBasket);
            if (createdorUpdatedBasket is null) return BadRequest(new ApiResponse(400));
            return Ok(createdorUpdatedBasket);
        }
        [HttpDelete]
        public async Task DeleteBasket(string id)
        {
          await  _basketRepository.DeleteBasketAsync(id);
        }
    }
}
