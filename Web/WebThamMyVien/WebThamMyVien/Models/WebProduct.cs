namespace WebThamMyVien.Models
{
    public class WebProduct
    {
        public List<ProductView> ListProductView { get; set; }
        public List<ProductTypeView> ListProductType { get; set; }

        public WebProduct()
        {
            ListProductView = new List<ProductView>();
            ListProductType = new List<ProductTypeView>();
        }
    }
}
