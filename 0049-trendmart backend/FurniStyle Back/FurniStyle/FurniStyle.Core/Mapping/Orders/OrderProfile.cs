using AutoMapper;
using Microsoft.Extensions.Configuration;
using FurniStyle.Core.DTOs.Orders;
using FurniStyle.Core.Entities.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurniStyle.Core.Mapping.Orders
{
    public class OrderProfile:Profile
    {
        public OrderProfile(IConfiguration configuration)
        {
            CreateMap<Order, OrderToReturnDTO>()
                .ForMember(p=>p.DelivaryMethodName,o=>o.MapFrom(p=>p.DelivaryMethod.ShortName))
                .ForMember(p=>p.DelivaryMethodCost,o=>o.MapFrom(p=>p.DelivaryMethod.Cost)).ReverseMap();

            CreateMap<Address, AddressDTO>().ReverseMap();


            CreateMap<OrderItem, OrderItemDTO>()
                .ForMember(p => p.ProductId, o => o.MapFrom(p => p.productItemOrder.ProductId))
                .ForMember(p => p.ProductName, o => o.MapFrom(p => p.productItemOrder.ProductName))
                .ForMember(p => p.ProductPictureURL, o => o.MapFrom(p => $"{configuration["BaseUrl"]}{p.productItemOrder.ProductPictureURL}")).ReverseMap();
        }
    }
}
