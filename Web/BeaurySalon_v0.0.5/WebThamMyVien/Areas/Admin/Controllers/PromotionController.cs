using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebThamMyVien.Interfaces;
using WebThamMyVien.Models;
using WebThamMyVien.Repository;
using X.PagedList;

namespace WebThamMyVien.Areas.Admin.Controllers
{
    public class PromotionController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public PromotionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
                var n = await _unitOfWork.Promotion.CreatePromotion(value);
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
                    await _unitOfWork.Action.CreateAction(IdUser, 1, "Thêm mới dữ liệu Khuyến Mãi {" + value.PromotionName + "}");
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
            var n = await _unitOfWork.Promotion.GetPromotion((int)id);
            return View(n);
        }

        [Area("Admin")]
        public async Task<IActionResult> Edit(PromotionDto? value)
        {
            // PromotionDto hợp lệ
            if (ModelState.IsValid)
            {
                // Update data in Db
                var n = await _unitOfWork.Promotion.UpdatePromotion(value);
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
                    await _unitOfWork.Action.CreateAction(IdUser, 2, "Thay đổi dữ liệu Khuyến Mãi {" + value.PromotionName + "}");
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
            string _mesg = "";
            int _status = 0;
            bool dl = false;
            if (ids != null)
            {
                var listA = await _unitOfWork.Service.GetAllService();
                var listB = await _unitOfWork.Product.GetAllProduct();
                foreach (var id in ids)
                {
                    int dll = 0;
                    foreach (var item in listA)
                    {
                        if (item.AppliedPromotionId == id)
                        {
                            dll += 1;
                            break;
                        }
                    }
                    foreach (var item in listB)
                    {
                        if (item.AppliedPromotionId == id)
                        {
                            dll += 1;
                            break;
                        }
                    }
                    if (dll == 0)
                    {
                        var n = await _unitOfWork.Promotion.GetPromotion(id);
                        dl = await _unitOfWork.Promotion.DeletePromotion(n);
                        //Tự động Thêm Hitory action
                        int IdUser = 0;
                        if (Request.Cookies.TryGetValue("IdUser", out string intString))
                        {
                            if (int.TryParse(intString, out int myIntValue))
                            {
                                IdUser = myIntValue;
                            }
                        }
                        await _unitOfWork.Action.CreateAction(IdUser, 3, "Xóa dữ liệu Khuyến Mãi {" + n.PromotionName + "}");
                    }
                    else
                    {
                        var n = await _unitOfWork.Promotion.GetPromotion(id);
                        if (_mesg == "")
                        {
                            _status = 2;
                            _mesg = "Không thể xóa thông tin " + ", " + n.PromotionName;
                        }
                        else
                        {
                            _mesg += ", " + n.PromotionName;
                        }

                    }
                }
            }
            if (dl && _status == 0)
            {
                _status = 1;
                _mesg = "Xóa thông tin thành công";
            }
            return Json(new { mesg = _mesg, status = _status });
        }

        [Area("Admin")]
        [HttpPost]
        public async Task<IActionResult> GetALL()
        {
            // Trong một action method hoặc service
            List<PromotionDto> list = await _unitOfWork.Promotion.GetAllPromotion() as List<PromotionDto>;

            string json = JsonConvert.SerializeObject(list);

            // Sử dụng biến json như bạn muốn, ví dụ:
            return Content(json, "application/json");
        }
    }
}
