using System;
using System.Collections.Generic;

namespace APICosmeticClinic.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
            ProductBranches = new HashSet<ProductBranch>();
            ProductImages = new HashSet<ProductImage>();
            ShoppingCartItems = new HashSet<ShoppingCartItem>();
        }

        public int Id { get; set; }
        public string? ProductName { get; set; }
        public string? Introduction { get; set; }
        public string? Functionality { get; set; }
        public string? Origin { get; set; }
        public int? SellingPrice { get; set; }
        public int? PurchasePrice { get; set; }
        public int? AppliedPromotionId { get; set; }
        public int? ProductTypeId { get; set; }
        public string? Other { get; set; }
        public bool? IsDelete { get; set; }
        public string? DateDelete { get; set; }

        public virtual Promotion? AppliedPromotion { get; set; }
        public virtual ProductType? ProductType { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<ProductBranch> ProductBranches { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
