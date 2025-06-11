using AutoMapper;
using FurniStyle.Core.DTOs.Furnis;
using FurniStyle.Core.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurniStyle.Core.Mapping.Furnis
{
    public class FurniProfile :Profile
    {
        public FurniProfile(IConfiguration configuration)
        {
            CreateMap<Furni,FurniDTO>().ForMember(p => p.CategoryName, options => options.MapFrom(p => p.Category.Name))
                                       .ForMember(p => p.RoomName, options => options.MapFrom(p => p.Room.Name))
                                       .ForMember(p => p.PictureUrl, options => options.MapFrom(p => $"{configuration["BaseUrl"]}{p.PictureUrl}"));
            
            CreateMap<Furni, FurniWithoutIncludesIdDTO>().ForMember(p => p.CategoryName, options => options.MapFrom(p => p.Category.Name))
                                       .ForMember(p => p.RoomName, options => options.MapFrom(p => p.Room.Name))
                                       .ForMember(p => p.PictureUrl, options => options.MapFrom(p => $"{configuration["BaseUrl"]}{p.PictureUrl}"));

            // تحويل Furni إلى FurniProcessesDTO
            CreateMap<Furni, FurniProcessesDTO>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.RoomName, opt => opt.MapFrom(src => src.Room.Name))
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom(src => $"{configuration["BaseUrl"]}{src.PictureUrl}"));

            // تحويل FurniProcessesDTO إلى Furni
            CreateMap<FurniProcessesDTO, Furni>()
                .ForMember(dest => dest.CategoryId, opt => opt.Ignore()) // سيتم تعيينه يدويًا
                .ForMember(dest => dest.RoomId, opt => opt.Ignore()) // سيتم تعيينه يدويًا
                .ForMember(dest => dest.Category, opt => opt.Ignore()) // تجنب تعيين كائن الـ Category
                .ForMember(dest => dest.Room, opt => opt.Ignore()); // تجنب تعيين كائن الـ Room
        }
    }
}
