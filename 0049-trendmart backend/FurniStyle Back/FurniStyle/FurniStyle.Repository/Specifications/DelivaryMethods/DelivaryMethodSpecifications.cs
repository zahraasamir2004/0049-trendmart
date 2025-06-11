using FurniStyle.Core.Entities.OrderEntities;
using FurniStyle.Repository.Specifications.ImplementingSpecifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurniStyle.Core.Specifications.DelivaryMethods
{
    public class DelivaryMethodSpecifications : Specification<DelivaryMethod,int>
    {
        public DelivaryMethodSpecifications(int id):base(p=>p.Id==id)
        {
            
        }
        public DelivaryMethodSpecifications()
        {
            
        }
    }
}
