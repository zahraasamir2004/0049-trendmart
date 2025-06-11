using AutoMapper;
using FurniStyle.Core.DTOs.Categories;
using FurniStyle.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurniStyle.Core.Mapping.Categories
{
   public class CategoryProfile :Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category,CategoryDTO>();
        }
    }
}
