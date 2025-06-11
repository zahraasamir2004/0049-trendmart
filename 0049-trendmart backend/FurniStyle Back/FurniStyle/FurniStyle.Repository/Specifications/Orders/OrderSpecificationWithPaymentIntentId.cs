using FurniStyle.Core.Entities.OrderEntities;
using FurniStyle.Repository.Specifications.ImplementingSpecifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurniStyle.Repository.Specifications.Orders
{
    public class OrderSpecificationWithPaymentIntentId:Specification<Order,int>
    {
        public OrderSpecificationWithPaymentIntentId(string paymentIntentId):base(p=>p.PaymentIntenId== paymentIntentId)
        {
            Includes.Add(p => p.DelivaryMethod);
            Includes.Add(p => p.Items);

        }
    }
}
