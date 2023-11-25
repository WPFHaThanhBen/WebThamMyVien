namespace WebThamMyVien.Models
{
    public class WebService
    {
        public List<ServiceTypeDto> ListServiceType { get; set; }
        public List<ServiceDto> ListService{ get; set; }
        public List<PostDto> ListPost1 { get; set; }
        public List<PostDto> ListPost2 { get; set; }

        public WebService()
        {
            ListServiceType = new List<ServiceTypeDto>();
            ListService = new List<ServiceDto>();
            ListPost1 = new List<PostDto>();
            ListPost2 = new List<PostDto>();
        }
    }
}
