using FurniStyle.Core.Entities;
using FurniStyle.Repository.Specifications.ImplementingSpecifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurniStyle.Repository.Specifications.Rooms
{
    public class RoomSpecifications:Specification<Room,int>
    {
        public RoomSpecifications(int id) :base(p=>p.Id==id)
        {
            
        }
        public RoomSpecifications()
        {
            
        }
        public RoomSpecifications(string? sort)
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
        public RoomSpecifications(string? seach, string? nullval) : base(p => p.Name == seach)
        {
        }
        public RoomSpecifications(int pageIndex, int pageSize)
        {
            ApplyPagenation(pageSize * (pageIndex - 1), pageSize);
        }
    }
}
