using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Diagnostics;
using WebThamMyVien.Interfaces;
using WebThamMyVien.Models;

namespace WebThamMyVien.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Area("Admin")]
        public async Task<IActionResult> Index()
        {
            TempData["menuDefault"] = "Post";
             var listPost = _unitOfWork.Post.GetAllPost();
            return View();
        }

        [Area("Admin")]
        [HttpPost]
        public async Task<IActionResult> GetALL()
        {
            // Trong một action method hoặc Post
            List<PostDto> list = await _unitOfWork.Post.GetAllPost() as List<PostDto>;

            string json = JsonConvert.SerializeObject(list);

            // Sử dụng biến json như bạn muốn, ví dụ:
            return Content(json, "application/json");
        }

        [Area("Admin")]
        [HttpPost]
        public async Task<IActionResult> CreatePostAJAX(string postDto)
        {
            PostDto postDto1 = JsonConvert.DeserializeObject<PostDto>(postDto);
            var createInvoice = await _unitOfWork.Post.CreatePost(postDto1);
            string json = JsonConvert.SerializeObject(createInvoice);
            // Sử dụng biến json như bạn muốn, ví dụ:
            return Content(json, "application/json");
        }

    }
}