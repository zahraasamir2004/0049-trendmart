using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurniStyle.Core.Entities.OrderEntities
{
    public class Order:BaseEntity<int>
    {
        public Order(string buyerEmail, Address shippingAddress,DelivaryMethod delivaryMethod, ICollection<OrderItem> items, decimal subTotal, string paymentIntenId)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            Items = items;
            SubTotal = subTotal;
            PaymentIntenId = paymentIntenId;
            DelivaryMethod = delivaryMethod;
        }
        public Order()
        {
            
        }

        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public Address ShippingAddress { get; set; }
        public ICollection<OrderItem> Items { get; set; }
        public decimal SubTotal { get; set; }
        public string PaymentIntenId { get; set; }

        public decimal GetTotal()=>SubTotal+DelivaryMethod.Cost;

        public int DelivaryMethodId { get; set; }
        public DelivaryMethod DelivaryMethod { get; set; }
    }
}
