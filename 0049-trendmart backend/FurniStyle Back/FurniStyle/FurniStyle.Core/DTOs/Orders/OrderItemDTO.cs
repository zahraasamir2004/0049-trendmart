using FurniStyle.Core.Entities.OrderEntities;

namespace FurniStyle.Core.DTOs.Orders
{
    public class OrderItemDTO
    {

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductPictureURL { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}