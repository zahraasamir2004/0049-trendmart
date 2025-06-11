﻿using FurniStyle.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurniStyle.Core.IRepositories
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?>GetBasketAsync(string basketId);
        Task<CustomerBasket?>UpdateBasketAsync(CustomerBasket customerBasket);
        Task<bool>DeleteBasketAsync(string basketId);
    }
}
