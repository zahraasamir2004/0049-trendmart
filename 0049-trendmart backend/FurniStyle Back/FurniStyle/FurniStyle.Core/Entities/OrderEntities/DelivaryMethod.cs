using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurniStyle.Core.Entities.OrderEntities
{
    public class DelivaryMethod:BaseEntity<int>
    {
        public string ShortName { get; set; }
        public string Description { get; set; }
        public string DelivaryTime { get; set; }
        public decimal Cost { get; set; }
    }
}
