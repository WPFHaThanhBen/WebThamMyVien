namespace WebThamMyVien.Models
{
    public class ProductView
    {
        public ProductView()
        {
            Images = new List<ProductImageDto>();
        }
        public ProductDto Product { get; set; }
        public int Promotion { get; set; }
        public List<ProductImageDto> Images { get; set; }

    }
}
