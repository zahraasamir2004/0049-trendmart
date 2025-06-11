using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurniStyle.Core.Entities
{
    public class Furni:BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string PictureUrl { get; set; }

        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public int? RoomId { get; set; }
        public virtual Room Room { get; set; }
    }
}
