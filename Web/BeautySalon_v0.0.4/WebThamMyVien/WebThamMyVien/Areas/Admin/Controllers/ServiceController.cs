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
        private readonly IServiceRepository _serviceRepository;
        private readonly IServiceTypeRepository _serviceTypeRepository;
        private readonly IPromotionRepository _promotionRepository;

        public ServiceController(IServiceRepository serviceRepository, IServiceTypeRepository serviceTypeRepository, IPromotionRepository promotionRepository)
        {
            _serviceRepository = serviceRepository;
            _serviceTypeRepository = serviceTypeRepository;
            _promotionRepository = promotionRepository;
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

            IEnumerable<ServiceTypeDto> serviceTypes = await _serviceTypeRepository.GetAllServiceType();
            serviceTypes = serviceTypes.ToList();
            ViewData["serviceType"] = new SelectList(serviceTypes, "Id", "TypeName");

            // lấy value cho Cbb PromotionDto
            IEnumerable<PromotionDto> promotions = await _promotionRepository.GetAllPromotion();
            promotions = promotions.ToList();
            ViewData["promotion"] = new SelectList(promotions, "Id", "PromotionName");

            // ServiceDto hợp lệ
            if (ModelState.IsValid)
            {
                // Add data in DB
                var n = await _serviceRepository.CreateService(_serviceDto);
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
            var n = await _serviceRepository.GetService((int)id);
            IEnumerable<ServiceTypeDto> serviceTypes = await _serviceTypeRepository.GetAllServiceType();
            serviceTypes = serviceTypes.ToList();
            ViewData["serviceType"] = new SelectList(serviceTypes, "Id", "TypeName",n.ServiceTypeId);

            // lấy value cho Cbb PromotionDto
            IEnumerable<PromotionDto> promotions = await _promotionRepository.GetAllPromotion();
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
                var n = await _serviceRepository.UpdateService(_serviceDto);
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
                        var n = await _serviceRepository.GetService(id);
                        dl = await _serviceRepository.DeleteService(n);
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
            List<ServiceDto> list = await _serviceRepository.GetAllService() as List<ServiceDto>;

            string json = JsonConvert.SerializeObject(list);

            // Sử dụng biến json như bạn muốn, ví dụ:
            return Content(json, "application/json");
        }

        [Area("Admin")]
        [HttpPost]
        public async Task<IActionResult> GetServiceType(int id)
        {
            // Trong một action method hoặc service
            ServiceTypeDto n = await _serviceTypeRepository.GetServiceType(id);
            string json = JsonConvert.SerializeObject(n);
            // Sử dụng biến json như bạn muốn, ví dụ:
            return Content(json, "application/json");
        }

        [Area("Admin")]
        [HttpPost]
        public async Task<IActionResult> GetPromotion(int id)
        {
            // Trong một action method hoặc service
            PromotionDto n = await _promotionRepository.GetPromotion(id);
            string json = JsonConvert.SerializeObject(n);
            // Sử dụng biến json như bạn muốn, ví dụ:
            return Content(json, "application/json");
        }
    }
}
