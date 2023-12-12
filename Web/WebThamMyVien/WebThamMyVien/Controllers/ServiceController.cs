using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Diagnostics;
using WebThamMyVien.Interfaces;
using WebThamMyVien.Models;

namespace WebThamMyVien.Controllers
{
    [Route("Service")]
    public class ServiceController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ServiceController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [Route("Index/{name}")]
        public async Task<IActionResult> Index([FromRoute] string name)
        {

            // Đọc giá trị của cookie "IdAccount" từ request
            string idAccountValue = Request.Cookies["IdAccount"];

            // Kiểm tra xem cookie có tồn tại không
            if (idAccountValue != null && idAccountValue != "")
            {
                int idAccount = Convert.ToInt32(idAccountValue);
                ViewData["IdAccount"] = idAccount;
            }
            else
            {
                ViewData["IdAccount"] = null;
            }

            WebService webService = new WebService();
            List<ServiceTypeDto> list = await _unitOfWork.ServiceType.GetAllServiceType() as List<ServiceTypeDto>;
            ViewData["menuDefault"] = "DichVu";
            ViewData["menuDefaultServiceTypeDto"] = list;
            var postType = await _unitOfWork.PostType.GetPostTypeByName(name);
            var listPost = await _unitOfWork.Post.GetPostByPostType(postType.Id) as List<PostDto>;
            webService.ListPost1 = listPost;
            return View(webService);
        }   
    }
}