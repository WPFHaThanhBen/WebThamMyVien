using System.ComponentModel.DataAnnotations;

namespace WebThamMyVien.Models
{
    public class ProductTypeView
    {
        public int Quantity { get; set; }
        public ProductTypeDto ProductType { get; set; }
    }
}
