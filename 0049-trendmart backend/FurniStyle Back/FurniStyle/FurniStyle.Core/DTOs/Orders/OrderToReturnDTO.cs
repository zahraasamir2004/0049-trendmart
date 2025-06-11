using FurniStyle.Core.Entities.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurniStyle.Core.DTOs.Orders
{
    public class OrderToReturnDTO
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public string Status { get; set; } 
        public AddressDTO ShippingAddress { get; set; }
        public string DelivaryMethodName { get; set; }
        public decimal DelivaryMethodCost { get; set; }
        public ICollection<OrderItemDTO> Items { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public string? PaymentIntenId { get; set; }=string.Empty;


    }
}
