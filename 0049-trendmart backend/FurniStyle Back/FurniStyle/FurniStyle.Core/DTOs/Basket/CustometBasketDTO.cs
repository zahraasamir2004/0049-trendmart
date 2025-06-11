using FurniStyle.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurniStyle.Core.DTOs.Basket
{
    public class CustometBasketDTO
    {
        public string Id { get; set; }
        public List<BasketItem> BasketItems { get; set; }
        public int? DelivaryMethodId { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
    }
}
