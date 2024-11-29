using AutoMapper;
using FinalProject.Core.Order_Aggregrate;
using FinalProject.Core.Services.Contract;
using FinalProjectApi.Dtos;
using FinalProjectApi.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }
        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
        {
            var address = _mapper.Map<AddressDto, Address>(orderDto.ShippingAddress);
            var order = await _orderService.CreateOrderAsync(orderDto.BuyerEmail, orderDto.BasketId, orderDto.DeliveryMethodId, address);
            if (order is null) return BadRequest(new ApiResponse(400));
            return Ok(_mapper.Map<OrderToReturnDto>(order));
        }
        [HttpPost]
        public async Task<ActionResult<IReadOnlyList<Order>>> GetOrdersForUser (string email)
        {

            var orders = _orderService.GetOrdersForUserAsync(email);
            return Ok(orders);
            }
        [HttpGet("id")]
        public async Task<ActionResult<Order>> GetOrderForUser(int id , string email)
        {
            var order = await _orderService.GetOrderByIdForUserAsync(id, email);
            if (order is null) return NotFound(new ApiResponse(404));
            return Ok(order);

        }
    }
}
