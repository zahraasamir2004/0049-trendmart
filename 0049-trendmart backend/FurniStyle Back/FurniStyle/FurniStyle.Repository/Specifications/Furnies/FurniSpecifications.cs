using FurniStyle.Core.Entities;
using FurniStyle.Repository.Specifications.ImplementingSpecifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurniStyle.Repository.Specifications.Furnies
{
    public class FurniSpecifications :Specification<Furni,int>
    {
        public FurniSpecifications(int id) :base(p=>p.Id==id)
        {
            ApplyIncludes();
        }
        public FurniSpecifications()
        {
            ApplyIncludes();
        }
        public FurniSpecifications(string? sort)
        {
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "NameAscending":
                        AddOrderBy(p => p.Name);
                        break;
                    case "NameDescending":
                        AddOrderByDescending(p => p.Name);
                        break;
                    case "PriceAscending":
                        AddOrderBy(p => p.Price);
                        break;
                    case "PriceDescending":
                        AddOrderByDescending(p => p.Price);
                        break;
                    case "QuantityAscending":
                        AddOrderBy(p => p.StockQuantity);
                        break;
                    case "QuantityDescending":
                        AddOrderByDescending(p => p.StockQuantity);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
            else
            {
                AddOrderBy(p => p.Name);
            }
            ApplyIncludes();
        }
        public FurniSpecifications(string? seach,string? nullval):base(p=>p.Name==seach)
        {
            ApplyIncludes();
        }
        public FurniSpecifications(string? category,string? room,string? nullval):base(p=>p.Category.Name== category || p.Room.Name==room)
        {
            ApplyIncludes();
        }
        public FurniSpecifications(decimal? price1 , decimal? price2):base(p => p.Price >= price1 && p.Price <= price2)
        {
            AddOrderBy(p => p.Price);
            ApplyIncludes();
        }
        public FurniSpecifications(int pageIndex, int pageSize)
        {
            ApplyPagenation(pageSize * (pageIndex - 1), pageSize);
            ApplyIncludes();
        }
        private void ApplyIncludes()
        {
            Includes.Add(p => p.Category);
            Includes.Add(p => p.Room);
        }
    }
}
