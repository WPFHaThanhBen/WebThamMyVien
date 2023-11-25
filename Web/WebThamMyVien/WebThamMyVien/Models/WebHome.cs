namespace WebThamMyVien.Models
{
    public class WebHome
    {
        public PostDto OnePost { get; set; }
        public List<PostDto> ListPost { get; set; }
        public List<PostDto> ListPost1 { get; set; }
        public List<PostDto> ListPost2 { get; set; }
        public List<ProductView> ListProductView { get; set; }

        public WebHome()
        {
            ListPost = new List<PostDto>();
            ListPost1 = new List<PostDto>();
            ListPost2 = new List<PostDto>();
            ListProductView = new List<ProductView>();
        }
    }
}
