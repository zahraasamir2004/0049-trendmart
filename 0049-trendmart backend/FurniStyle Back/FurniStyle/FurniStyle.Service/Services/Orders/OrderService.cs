using FurniStyle.Core;
using FurniStyle.Core.Entities;
using FurniStyle.Core.Entities.OrderEntities;
using FurniStyle.Core.IServices;
using FurniStyle.Core.IServices.Payments;
using FurniStyle.Core.IUnitOfWork;
using FurniStyle.Core.Specifications.DelivaryMethods;
using FurniStyle.Core.Specifications.Orders;
using FurniStyle.Repository.Specifications.Furnies;
using FurniStyle.Repository.Specifications.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurniStyle.Service.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketService _basketService;
        private readonly IPaymentService _paymentService;

        public OrderService(IUnitOfWork unitOfWork,IBasketService basketService,IPaymentService paymentService)
        {
            _unitOfWork = unitOfWork;
            _basketService = basketService;
            _paymentService = paymentService;
        }
        
        public async Task<Order> CreateOrderAsync(string buyerEmail, string basketId, int delivaryMethodId, Address shippingAddress)
        {
            var basket = await _basketService.GetBasketByIdAsync(basketId);
            if (basket is null) return null;
            var orderItems = new List<OrderItem>();
            if (basket.BasketItems.Count() > 0)
            {
                foreach(var item in basket.BasketItems)
                {
                    var specifications = new FurniSpecifications(item.Id);
                    var product = await _unitOfWork.Repository<Furni, int>().GetAsync(specifications);
                    var productOrderItem = new ProductItemOrder(product.Id,product.Name,product.PictureUrl);
                    var orderItem = new OrderItem(productOrderItem, product.Price,item.Quantatiy);
                    orderItems.Add(orderItem);
                }

            }

            var specification = new DelivaryMethodSpecifications();
            var delivaryMethod = await _unitOfWork.Repository<DelivaryMethod, int>().GetAsync(specification);

            var subTotal = orderItems.Sum(p=>p.Price * p.Quantity);
            if(string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var specificationIntentId = new OrderSpecificationWithPaymentIntentId(basket.PaymentIntentId);
                var exOrder= await _unitOfWork.Repository<Order, int>().GetAsync(specificationIntentId);
                _unitOfWork.Repository<Order, int>().Delete(exOrder);
            }
            var bsketdto = await _paymentService.CreateUpdatePaymentIntentAsync(basketId);

            var order = new Order(buyerEmail,shippingAddress, delivaryMethod, orderItems, subTotal, bsketdto.PaymentIntentId);

            await _unitOfWork.Repository<Order, int>().AddAsync(order);
            int result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return null;
            return order;

        }

        public async Task<Order?> GetOrderByIdForSpecificUserAsync(string buyerEmail, int orderId)
        {
            var specification = new OrderSpecifications(buyerEmail,orderId);
            var order = await _unitOfWork.Repository<Order,int>().GetAsync(specification);
            if(order is null) return null;
            return order;
        }

        public async Task<IEnumerable<Order>?> GetOrdersForSpecificUserAsync(string buyerEmail)
        {
            var specification = new OrderSpecifications(buyerEmail);
            var orders = await _unitOfWork.Repository<Order, int>().GetAllAsync(specification);
            if (orders is null) return null;
            return orders;
        }

        public async Task<IEnumerable<DelivaryMethod>?> GetAllDelivaryMethodsAsync()
        {
            var spec = new DelivaryMethodSpecifications();
            var delivaryMethods = await _unitOfWork.Repository<DelivaryMethod, int>().GetAllAsync(spec);
            if (delivaryMethods is null) return null;
            return delivaryMethods;
        }

    }
}
