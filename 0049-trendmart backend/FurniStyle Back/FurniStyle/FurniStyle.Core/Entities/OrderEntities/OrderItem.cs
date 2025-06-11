using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurniStyle.Core.Entities.OrderEntities
{
    public class OrderItem:BaseEntity<int>
    {
        public OrderItem()
        {
            
        }
        public OrderItem(ProductItemOrder productItemOrder, decimal price, int quantity)
        {
            this.productItemOrder = productItemOrder;
            Price = price;
            Quantity = quantity;
        }

        public ProductItemOrder productItemOrder { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
