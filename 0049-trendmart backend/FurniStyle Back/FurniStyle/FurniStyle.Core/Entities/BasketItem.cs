using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurniStyle.Core.Entities
{
    public class BasketItem
    {
        public int Id { get; set; }
        public string FurniName { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public string Category { get; set; }
        public string Room { get; set; }
        public decimal Price { get; set; }
        public int Quantatiy { get; set; }
    }
}
