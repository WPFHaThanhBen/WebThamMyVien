using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging.Signing;
using WebThamMyVien.Interfaces;
using WebThamMyVien.Models;
using WebThamMyVien.Repository;
using X.PagedList;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace WebThamMyVien.Areas.Admin.Controllers
{
    public class ServiceTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ServiceTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Area("Admin")]
        public async Task<IActionResult> Index()
        {
            TempData["menu"] = "ServiceType";
            return View();
        }

        [Area("Admin")]
        public async Task<IActionResult> Create(ServiceTypeDto? value)
        {
            // Khởi tạo Menu (bắt buộc)
            TempData["menu"] = "ServiceType";
            // ServiceTypeDto hợp lệ
            if (ModelState.IsValid)
            {
                // Add data in DB
                var n = await _unitOfWork.ServiceType.CreateServiceType(value);
                PostTypeDto postType = new PostTypeDto();
                postType.Id = 0;
                postType.TypeName = value.TypeName;
                postType.Other = "Hệ thống tự động tạo " + value.TypeName + "này, Vui lòng không thay đổi thông tin này để tránh ảnh hưởng đến hệ thống";
                var m = await _unitOfWork.PostType.CreatePostType(postType);
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
                    await _unitOfWork.Action.CreateAction(IdUser, 1, "Thêm mới dữ liệu Loại Dịch Vụ {" + value.TypeName + "}");
                    TempData["success"] = "Thêm mới thông tin thành công";
                    return RedirectToAction("Index");
                }
                // Add status is error
                else
                {
                    TempData["error"] = "Thêm mới thông tin thất bại";
                }
            }
            // ServiceTypeDto không hợp lệ
            else
            {
                if (value.TypeName == "" || value.TypeName == null)
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
            TempData["menu"] = "ServiceType";
            TempData["EditServiceType"] = id;
            // Get ServiceType in DB
            var n = await _unitOfWork.ServiceType.GetServiceType((int)id);
            return View(n);
        }

        [Area("Admin")]
        public async Task<IActionResult> Edit(ServiceTypeDto? value)
        {
            // ServiceTypeDto hợp lệ
            if (ModelState.IsValid)
            {
                // Update data in Db
                var serviceCu = await _unitOfWork.ServiceType.GetServiceType(value.Id);
                var n = await _unitOfWork.ServiceType.UpdateServiceType(value);
                PostTypeDto postType = await _unitOfWork.PostType.GetPostTypeByName(serviceCu.TypeName);
                string nameType = value.TypeName;
                postType.TypeName = nameType;
                postType.Other = "Hệ thống cập nhật đã " + value.TypeName + "này, Vui lòng không thay đổi thông tin này để tránh ảnh hưởng đến hệ thống";
                var m = await _unitOfWork.PostType.UpdatePostType(postType);
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
                    await _unitOfWork.Action.CreateAction(IdUser, 2, "Thay đổi dữ liệu Loại Dịch Vụ {" + value.TypeName + "}");
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
            // ServiceTypeDto không hợp lệ
            else
            {
                TempData["warning"] = "Thông tin không hợp lệ";
            }

            return RedirectToAction("Detail", new { id = TempData["EditServiceType"] });
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
                foreach (var id in ids)
                {
                    int dll = 0;
                    foreach (var item in listA)
                    {
                        if (item.ServiceTypeId == id)
                        {
                            dll += 1;
                            break;
                        }
                    }
                    if (dll == 0)
                    {
                        
                        var n = await _unitOfWork.ServiceType.GetServiceType(id);
                        dl = await _unitOfWork.ServiceType.DeleteServiceType(n);
                        //Tự động Thêm Hitory action
                        int IdUser = 0;
                        if (Request.Cookies.TryGetValue("IdUser", out string intString))
                        {
                            if (int.TryParse(intString, out int myIntValue))
                            {
                                IdUser = myIntValue;
                            }
                        }
                        await _unitOfWork.Action.CreateAction(IdUser, 3, "Xóa dữ liệu Loại Người Dùng {" + n.TypeName + "}");
                    }
                    else
                    {
                        var n = await _unitOfWork.ServiceType.GetServiceType(id);
                        if (_mesg == "")
                        {
                            _status = 2;
                            _mesg = "Không thể xóa thông tin " + ", " + n.TypeName;
                        }
                        else
                        {
                            _mesg += ", " + n.TypeName;
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
            List<ServiceTypeDto> list = await _unitOfWork.ServiceType.GetAllServiceType() as List<ServiceTypeDto>;

            string json = JsonConvert.SerializeObject(list);

            // Sử dụng biến json như bạn muốn, ví dụ:
            return Content(json, "application/json");
        }
    }
}
