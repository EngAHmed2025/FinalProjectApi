using FinalProject.Core;
using FinalProject.Core.Models;
using FinalProject.Core.Order_Aggregrate;
using FinalProject.Core.Repositories.Contract;
using FinalProject.Core.Services.Contract;
using FinalProject.Core.Specifictions.OrderSpecifiction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitofWork _unitofWork;
     

        public OrderService(IBasketRepository basketRepository, IUnitofWork unitofWork  )
        {
           _basketRepository = basketRepository;
            _unitofWork = unitofWork;
           
        }
        public async Task<Order> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Address shippingAddress )
        {
            var basket = await _basketRepository.GetBasketAsync(basketId);
            var orderItems = new List<OrderItems>();
            if (basket?.Items?.Count > 0)
            
            {

                foreach (var item in basket.Items)
                {
                    var product = await _unitofWork.Repository<Product>().GetAsync(item.Id);
                    var productItemOrderd = new ProductItemOrdered(item.Id, product.Name, product.PictureUrl);
                    var orderItem = new OrderItems(productItemOrderd, product.Price, item.Quantity);
                    orderItems.Add(orderItem);
                }
            }

            var subTotal = orderItems.Sum(orderItems => orderItems.Price * orderItems.Quantity);
            var deliveryMethod = await _unitofWork.Repository<DeliveryMethod>().GetAsync(deliveryMethodId);
            var order = new Order(buyerEmail, shippingAddress, deliveryMethod, orderItems, subTotal);
           await _unitofWork.Repository<Order>().AddAsync(order);

        var result =  await  _unitofWork.CompleteAsync();
            if (result <= 0) return null;
            return order;
        }

        public Task<Order> GetOrderByIdForUserAsync(int orderId, string buyerEmail)
        {
            var orderRepo = _unitofWork.Repository<Order>();
            var spec = new OrderSpecifications(orderId,buyerEmail);
            var order = orderRepo.GetWithSpecAsync(spec);
            return order;
        }

        public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var orderRepo = _unitofWork.Repository<Order>();
            var spec = new OrderSpecifications(buyerEmail);
            var orders =  orderRepo.GetAllWithSpecAsync(spec);
            return orders;
        }
    }
}
