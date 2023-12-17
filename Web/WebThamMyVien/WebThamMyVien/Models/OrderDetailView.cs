namespace WebThamMyVien.Models
{
    public class OrderDetailView:OrderDetailDto
    {
        public string productName { get; set; }

        public List<ProductImageDto> Images { get; set; }
    }
}
