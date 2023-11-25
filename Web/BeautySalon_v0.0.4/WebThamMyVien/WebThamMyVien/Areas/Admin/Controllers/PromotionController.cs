using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebThamMyVien.Interfaces;
using WebThamMyVien.Models;
using X.PagedList;

namespace WebThamMyVien.Areas.Admin.Controllers
{
    public class PromotionController : Controller
    {
        private readonly IPromotionRepository _PromotionRepository;

        public PromotionController(IPromotionRepository PromotionRepository)
        {
            _PromotionRepository = PromotionRepository;
        }

        [Area("Admin")]
        public async Task<IActionResult> Index()
        {
            TempData["menu"] = "Promotion";
            return View();
        }

        [Area("Admin")]
        public async Task<IActionResult> Create(PromotionDto? value)
        {
            // Khởi tạo Menu (bắt buộc)
            TempData["menu"] = "Promotion";
            // PromotionDto hợp lệ
            if (ModelState.IsValid)
            {
                // Add data in DB
                var n = await _PromotionRepository.CreatePromotion(value);
                // Add status is success
                if (n)
                {
                    TempData["success"] = "Thêm mới thông tin thành công";
                    return RedirectToAction("Index");
                }
                // Add status is error
                else
                {
                    TempData["error"] = "Thêm mới thông tin thất bại";
                }
            }
            // PromotionDto không hợp lệ
            else
            {
                if (value.PromotionName == "" || value.PromotionName == null)
                {
                    return View(value);
                }
                TempData["warning"] = "Thông tin không hợp lệ";
            }
            return View(value);
        }

        [Area("Admin")]
        public async Task<IActionResult> Detail(int? id = 1)
        {
            // Khởi tạo Menu (bắt buộc)
            TempData["menu"] = "Promotion";
            TempData["EditPromotion"] = id;
            // Get Promotion in DB
            var n = await _PromotionRepository.GetPromotion((int)id);
            return View(n);
        }

        [Area("Admin")]
        public async Task<IActionResult> Edit(PromotionDto? value)
        {
            // PromotionDto hợp lệ
            if (ModelState.IsValid)
            {
                // Update data in Db
                var n = await _PromotionRepository.UpdatePromotion(value);
                // Update status is success
                if (n)
                {
                    //Trở về Index và thông báo thành công
                    TempData["success"] = "Cập nhật thông tin thành công";
                    return RedirectToAction("Index");
                }
                // Update status is error
                else
                {
                    // Thông báo thất bại
                    TempData["error"] = "Cập nhật thông tin thất bại";
                }
            }
            // PromotionDto không hợp lệ
            else
            {
                TempData["warning"] = "Thông tin không hợp lệ";
            }

            return RedirectToAction("Detail", new { id = TempData["EditPromotion"] });
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
                    var n = await _PromotionRepository.GetPromotion(id);
                    dl = await _PromotionRepository.DeletePromotion(n);

                }
            }
            if (dl)
            {
                TempData["success"] = "Xóa thông tin thành công";
            }
            return RedirectToAction("Index");
        }

        [Area("Admin")]
        [HttpPost]
        public async Task<IActionResult> GetALL()
        {
            // Trong một action method hoặc service
            List<PromotionDto> list = await _PromotionRepository.GetAllPromotion() as List<PromotionDto>;

            string json = JsonConvert.SerializeObject(list);

            // Sử dụng biến json như bạn muốn, ví dụ:
            return Content(json, "application/json");
        }
    }
}
