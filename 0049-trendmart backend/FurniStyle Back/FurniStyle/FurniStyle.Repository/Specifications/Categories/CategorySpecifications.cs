using FurniStyle.Core.Entities;
using FurniStyle.Repository.Specifications.ImplementingSpecifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurniStyle.Repository.Specifications.Categories
{
    public class CategorySpecifications:Specification<Category,int>
    {
        public CategorySpecifications(int id):base(p=>p.Id==id)
        {
            
        }
        public CategorySpecifications()
        {
            
        }
        public CategorySpecifications(string? sort)
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
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
            else
            {
                AddOrderBy(p => p.Name);
            }

        }
        public CategorySpecifications(string? seach, string? nullval) : base(p => p.Name == seach)
        {
        }
        public CategorySpecifications(int pageIndex, int pageSize)
        {
            ApplyPagenation(pageSize * (pageIndex - 1), pageSize);
        }
    }
}
