using AutoMapper;
using FurniStyle.Core.DTOs.Basket;
using FurniStyle.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurniStyle.Core.Mapping.Basket
{
    public class BasketProfile :Profile
    {
        public BasketProfile()
        {
            CreateMap<CustomerBasket, CustometBasketDTO>().ReverseMap();
        }
    }
}
