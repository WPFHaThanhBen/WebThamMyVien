using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Diagnostics;
using WebThamMyVien.Interfaces;
using WebThamMyVien.Models;

namespace WebThamMyVien.Controllers
{
    [Route("Post")]
    public class PostController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public PostController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [Route("Index/{id}/{menu}")]
        public async Task<IActionResult> Index([FromRoute] int id, string menu)
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

            string ddd = menu;
            PostDto webPost = new PostDto();
            List<ServiceTypeDto> list = await _unitOfWork.ServiceType.GetAllServiceType() as List<ServiceTypeDto>;
            ViewData["menuDefault"] = ddd;
            ViewData["menuDefaultServiceTypeDto"] = list;

            var post = await _unitOfWork.Post.GetPost(id);
            webPost = post;

            // Gán giá trị vào ViewBag hoặc ViewModel
            ViewBag.SummernoteContent = webPost.Content;

            return View(webPost);
        }
    }
}