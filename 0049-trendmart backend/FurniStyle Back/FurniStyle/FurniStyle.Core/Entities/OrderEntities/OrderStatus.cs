using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FurniStyle.Core.Entities.OrderEntities    
{
    public enum OrderStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,

        [EnumMember(Value = "Payment Recieved")]
        PaymentRecieved,

        [EnumMember(Value = "Payment Faild")]
        PaymentFaild,
    }
}
