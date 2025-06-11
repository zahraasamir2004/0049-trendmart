namespace FurniStyle.Core.Entities.OrderEntities
{
    public class ProductItemOrder
    {
        public ProductItemOrder(int productId, string productName, string productPictureURL)
        {
            ProductId = productId;
            ProductName = productName;
            ProductPictureURL = productPictureURL;
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductPictureURL { get; set; }
    }
}