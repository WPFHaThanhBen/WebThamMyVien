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
        private readonly IServiceTypeRepository _ServiceTypeRepository;

        public ServiceTypeController(IServiceTypeRepository ServiceTypeRepository)
        {
            _ServiceTypeRepository = ServiceTypeRepository;
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
                var n = await _ServiceTypeRepository.CreateServiceType(value);
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
            var n = await _ServiceTypeRepository.GetServiceType((int)id);
            return View(n);
        }

        [Area("Admin")]
        public async Task<IActionResult> Edit(ServiceTypeDto? value)
        {
            // ServiceTypeDto hợp lệ
            if (ModelState.IsValid)
            {
                // Update data in Db
                var n = await _ServiceTypeRepository.UpdateServiceType(value);
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
            bool dl = false;
            if (ids != null)
            {
                foreach (var id in ids)
                {
                    var n = await _ServiceTypeRepository.GetServiceType(id);
                    dl = await _ServiceTypeRepository.DeleteServiceType(n);

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
            List<ServiceTypeDto> list = await _ServiceTypeRepository.GetAllServiceType() as List<ServiceTypeDto>;

            string json = JsonConvert.SerializeObject(list);

            // Sử dụng biến json như bạn muốn, ví dụ:
            return Content(json, "application/json");
        }
    }
}
