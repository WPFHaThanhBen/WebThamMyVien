using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using WebThamMyVien.Interfaces;
using WebThamMyVien.Models;

namespace WebThamMyVien.Areas.Admin.Controllers
{
    public class PostController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public PostController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Area("Admin")]
        public async Task<IActionResult> Index()
        {
            TempData["menu"] = "Post";
            return View();
        }

        [Area("Admin")]
        public async Task<IActionResult> Create(PostDto? _PostDto)
        {
            // Khởi tạo Menu (bắt buộc)
            TempData["menu"] = "Post";

            IEnumerable<PostTypeDto> PostTypes = await _unitOfWork.PostType.GetAllPostType();
            PostTypes = PostTypes.ToList();
            ViewData["PostType"] = new SelectList(PostTypes, "Id", "TypeName");

            // PostDto hợp lệ
            if (ModelState.IsValid)
            {
                // Add data in DB
                var n = await _unitOfWork.Post.CreatePost(_PostDto);
                // Add status is success
                if (n)
                {
                    //Tự động Thêm Hitory action
                    int IdUser = 0;
                    if (Request.Cookies.TryGetValue("IdUser", out string intString))
                    {
                        if (int.TryParse(intString, out int myIntValue))
                        {
                            IdUser = myIntValue;
                        }
                    }
                    await _unitOfWork.Action.CreateAction(IdUser, 1, "Thêm mới dữ liệu Dịch Vụ {" + _PostDto.Title + "}");
                    TempData["success"] = "Thêm mới thông tin thành công";
                    return RedirectToAction("Index", new { area = "Admin", controller = "Post" });
                }
                // Add status is error
                else
                {
                    TempData["error"] = "Thêm mới thông tin thất bại";
                }
            }
            // PostDto không hợp lệ
            else
            {
                if (_PostDto.Title == "" || _PostDto.Title == null)
                {
                    return View(_PostDto);
                }
                TempData["warning"] = "Thông tin không hợp lệ";
            }
            return View(_PostDto);
        }

        [Area("Admin")]
        public async Task<IActionResult> Detail(int? id = 1)
        {
            var n = await _unitOfWork.Post.GetPost((int)id);
            IEnumerable<PostTypeDto> PostTypes = await _unitOfWork.PostType.GetAllPostType();
            PostTypes = PostTypes.ToList();
            ViewData["PostType"] = new SelectList(PostTypes, "Id", "TypeName", n.PostTypeId);
            // Khởi tạo Menu (bắt buộc)
            TempData["menu"] = "Post";
            TempData["EditID"] = id;
            // Get Post in DB

            return View(n);
        }

        [Area("Admin")]
        public async Task<IActionResult> Edit(PostDto? _PostDto)
        {
            // _PostDto hợp lệ
            if (ModelState.IsValid)
            {
                // Update data in Db
                var n = await _unitOfWork.Post.UpdatePost(_PostDto);
                // Update status is success
                if (n)
                {
                    //Tự động Thêm Hitory action
                    int IdUser = 0;
                    if (Request.Cookies.TryGetValue("IdUser", out string intString))
                    {
                        if (int.TryParse(intString, out int myIntValue))
                        {
                            IdUser = myIntValue;
                        }
                    }
                    await _unitOfWork.Action.CreateAction(IdUser, 2, "Thay đổi dữ liệu Dịch Vụ {" + _PostDto.Title + "}");
                    //Trở về Index và thông báo thành công
                    TempData["success"] = "Cập nhật thông tin thành công";
                    return RedirectToAction("Index", new { area = "Admin", controller = "Post" });
                }
                // Update status is error
                else
                {
                    // Thông báo thất bại
                    TempData["error"] = "Cập nhật thông tin thất bại";
                }
            }
            // _PostDto không hợp lệ
            else
            {
                TempData["warning"] = "Thông tin không hợp lệ";
            }

            return RedirectToAction("Detail", new { id = TempData["EditID"] });
        }

        [Area("Admin")]
        public async Task<IActionResult> Delete(int[]? ids)
        {
            //Danh sách id cần xóa không rỗng
            bool dl = false;
            if (ids != null)
            {
                foreach (var id in ids)
                {
                    var n = await _unitOfWork.Post.GetPost(id);
                    dl = await _unitOfWork.Post.DeletePost(n);
                    //Tự động Thêm Hitory action
                    int IdUser = 0;
                    if (Request.Cookies.TryGetValue("IdUser", out string intString))
                    {
                        if (int.TryParse(intString, out int myIntValue))
                        {
                            IdUser = myIntValue;
                        }
                    }
                    await _unitOfWork.Action.CreateAction(IdUser, 3, "Xóa dữ liệu Dịch Vụ {" + n.Title + "}");
                }
            }
            if (dl)
            {
                TempData["success"] = "Xóa thông tin thành công";
            }
            return RedirectToAction("Index", new { area = "Admin", controller = "Post" });
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
        public async Task<IActionResult> GetPostType(int id)
        {
            // Trong một action method hoặc Post
            PostTypeDto n = await _unitOfWork.PostType.GetPostType(id);
            string json = JsonConvert.SerializeObject(n);
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
