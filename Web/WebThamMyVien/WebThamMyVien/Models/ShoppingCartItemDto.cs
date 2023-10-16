namespace WebThamMyVien.Models
{
    public class ShoppingCartItemDto
    {
        public int Id { get; set; }
        public int? ShoppingCartId { get; set; }
        public int? ProductId { get; set; }
        public int? ServiceId { get; set; }
        public int? Quantity { get; set; }
        public int? Price { get; set; }
    }
}
