using FurniStyle.Core.DTOs.Basket;
using FurniStyle.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurniStyle.Core.IServices.Payments
{
    public interface IPaymentService
    {
        Task<CustometBasketDTO> CreateUpdatePaymentIntentAsync(string basketId);
    }
}
