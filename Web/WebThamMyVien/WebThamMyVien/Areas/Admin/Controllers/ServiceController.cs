using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using WebThamMyVien.Interfaces;
using WebThamMyVien.Models;
using WebThamMyVien.Repository;
using X.PagedList;

namespace WebThamMyVien.Areas.Admin.Controllers
{
    public class ServiceController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        public ServiceController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Area("Admin")]
        public async Task<IActionResult> Index()
        {
            TempData["menu"] = "Service";
            return View();
        }

        [Area("Admin")]
        public async Task<IActionResult> Create(ServiceDto? _serviceDto)
        {
            // Khởi tạo Menu (bắt buộc)
            TempData["menu"] = "Service";

            IEnumerable<ServiceTypeDto> serviceTypes = await _unitOfWork.ServiceType.GetAllServiceType();
            serviceTypes = serviceTypes.ToList();
            ViewData["serviceType"] = new SelectList(serviceTypes, "Id", "TypeName");

            // lấy value cho Cbb PromotionDto
            IEnumerable<PromotionDto> promotions = await _unitOfWork.Promotion.GetAllPromotion();
            promotions = promotions.ToList();
            ViewData["promotion"] = new SelectList(promotions, "Id", "PromotionName");

            // ServiceDto hợp lệ
            if (ModelState.IsValid)
            {
                // Add data in DB
                var n = await _unitOfWork.Service.CreateService(_serviceDto);
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
                    await _unitOfWork.Action.CreateAction(IdUser, 1, "Thêm mới dữ liệu Dịch Vụ {" + _serviceDto.ServiceName + "}");
                    TempData["success"] = "Thêm mới thông tin thành công";
                    return RedirectToAction("Index");
                }
                // Add status is error
                else
                {
                    TempData["error"] = "Thêm mới thông tin thất bại";
                }
            }
            // ServiceDto không hợp lệ
            else
            {
                if (_serviceDto.ServiceName == "" || _serviceDto.ServiceName == null)
                {
                    return View(_serviceDto);
                }
                TempData["warning"] = "Thông tin không hợp lệ";
            }
            return View(_serviceDto);
        }

        [Area("Admin")]
        public async Task<IActionResult> Detail(int? id = 1)
        {
            var n = await _unitOfWork.Service.GetService((int)id);
            IEnumerable<ServiceTypeDto> serviceTypes = await _unitOfWork.ServiceType.GetAllServiceType();
            serviceTypes = serviceTypes.ToList();
            ViewData["serviceType"] = new SelectList(serviceTypes, "Id", "TypeName",n.ServiceTypeId);

            // lấy value cho Cbb PromotionDto
            IEnumerable<PromotionDto> promotions = await _unitOfWork.Promotion.GetAllPromotion();
            promotions = promotions.ToList();
            ViewData["promotion"] = new SelectList(promotions, "Id", "PromotionName",n.AppliedPromotionId);
            // Khởi tạo Menu (bắt buộc)
            TempData["menu"] = "Service";
            TempData["EditID"] = id;
            // Get Service in DB
            
            return View(n);
        }

        [Area("Admin")]
        public async Task<IActionResult> Edit(ServiceDto? _serviceDto)
        {
            // _serviceDto hợp lệ
            if (ModelState.IsValid)
            {
                // Update data in Db
                var n = await _unitOfWork.Service.UpdateService(_serviceDto);
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
                    await _unitOfWork.Action.CreateAction(IdUser, 2, "Thay đổi dữ liệu Dịch Vụ {" + _serviceDto.ServiceName + "}");
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
            // _serviceDto không hợp lệ
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
                        var n = await _unitOfWork.Service.GetService(id);
                        dl = await _unitOfWork.Service.DeleteService(n);
                    //Tự động Thêm Hitory action
                    int IdUser = 0;
                    if (Request.Cookies.TryGetValue("IdUser", out string intString))
                    {
                        if (int.TryParse(intString, out int myIntValue))
                        {
                            IdUser = myIntValue;
                        }
                    }
                    await _unitOfWork.Action.CreateAction(IdUser, 3, "Xóa dữ liệu Dịch Vụ {" + n.ServiceName + "}");
                }
            }
            if(dl){
                TempData["success"] = "Xóa thông tin thành công";
            }
            return RedirectToAction("Index");
        }

        [Area("Admin")]
        [HttpPost]
        public async Task<IActionResult> GetALL()
        {
            // Trong một action method hoặc service
            List<ServiceDto> list = await _unitOfWork.Service.GetAllService() as List<ServiceDto>;

            string json = JsonConvert.SerializeObject(list);

            // Sử dụng biến json như bạn muốn, ví dụ:
            return Content(json, "application/json");
        }

        [Area("Admin")]
        [HttpPost]
        public async Task<IActionResult> GetServiceType(int id)
        {
            // Trong một action method hoặc service
            ServiceTypeDto n = await _unitOfWork.ServiceType.GetServiceType(id);
            string json = JsonConvert.SerializeObject(n);
            // Sử dụng biến json như bạn muốn, ví dụ:
            return Content(json, "application/json");
        }

        [Area("Admin")]
        [HttpPost]
        public async Task<IActionResult> GetPromotion(int id)
        {
            // Trong một action method hoặc service
            PromotionDto n = await _unitOfWork.Promotion.GetPromotion(id);
            string json = JsonConvert.SerializeObject(n);
            // Sử dụng biến json như bạn muốn, ví dụ:
            return Content(json, "application/json");
        }
    }
}
