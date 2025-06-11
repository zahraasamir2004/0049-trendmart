using FurniStyle.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FurniStyle.Core.DTOs.Basket;

namespace FurniStyle.Core.IServices
{
    public interface IBasketService
    {
        Task<CustometBasketDTO> GetBasketByIdAsync(string id);
        Task<CustomerBasket> UpdateOrCreateBasketAsync(CustometBasketDTO custometBasketDTO);
        Task DeletingBasketAsync(string id);
    }
}
