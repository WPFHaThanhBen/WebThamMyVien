using System.ComponentModel.DataAnnotations;
namespace WebThamMyVien.Models
{
    public class ShoppingCartItemView
    {
        public int Id { get; set; }
        public int? ShoppingCartId { get; set; }
        public ProductView? ProductView { get; set; }
        public int? Quantity { get; set; }
        public int? Price { get; set; }
    }
}
