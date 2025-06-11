using FurniStyle.Core.Entities.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurniStyle.Core.IServices
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string buyerEmail, string basketId, int delivaryMethodId,Address shippingAddress);
        Task<IEnumerable<Order>?> GetOrdersForSpecificUserAsync(string buyerEmail);
        Task<Order?> GetOrderByIdForSpecificUserAsync(string buyerEmail,int orderId);
        Task<IEnumerable<DelivaryMethod>?> GetAllDelivaryMethodsAsync();



    }
}
