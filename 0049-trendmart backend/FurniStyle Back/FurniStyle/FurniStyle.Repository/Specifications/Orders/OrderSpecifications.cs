using FurniStyle.Core.Entities.OrderEntities;
using FurniStyle.Repository.Specifications.ImplementingSpecifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurniStyle.Core.Specifications.Orders
{
    public class OrderSpecifications:Specification<Order,int>
    {
        public OrderSpecifications(string buyerEmail,int orderId):base(p=>p.BuyerEmail==buyerEmail&& p.Id==orderId)
        {
            Includes.Add(p => p.DelivaryMethod);
            Includes.Add(p => p.Items);
        }
        public OrderSpecifications(string buyerEmail) : base(p => p.BuyerEmail == buyerEmail)
        {
            Includes.Add(p => p.DelivaryMethod);
            Includes.Add(p => p.Items);
        }
    }
}
