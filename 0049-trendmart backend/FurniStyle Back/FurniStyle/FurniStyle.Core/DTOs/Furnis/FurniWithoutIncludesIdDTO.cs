using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurniStyle.Core.DTOs.Furnis
{
    public class FurniWithoutIncludesIdDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string PictureUrl { get; set; }
        public string CategoryName { get; set; }
        public string RoomName { get; set; }
    }
}
