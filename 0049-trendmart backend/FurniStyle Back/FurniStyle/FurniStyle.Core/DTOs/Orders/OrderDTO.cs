using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurniStyle.Core.DTOs.Orders
{
    public class OrderDTO
    {
        public string BasketId { get; set; }
        public int DelivaryMethodId { get; set; }
        public AddressDTO ShipToAddress { get; set; }
    }
}
