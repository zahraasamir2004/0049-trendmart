using AutoMapper;
using FurniStyle.Core.DTOs.Authentications;
using FurniStyle.Core.DTOs.Basket;
using FurniStyle.Core.Entities;
using FurniStyle.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurniStyle.Core.Mapping.Autentication
{
    public class AuthenticationProfile : Profile
    {
        public AuthenticationProfile()
        {
            CreateMap<Address, AddressDTO>().ReverseMap();
        }
    }
}