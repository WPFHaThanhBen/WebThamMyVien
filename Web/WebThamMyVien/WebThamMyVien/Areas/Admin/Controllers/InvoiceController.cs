using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using WebThamMyVien.Interfaces;
using WebThamMyVien.Models;

namespace WebThamMyVien.Areas.Admin.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public InvoiceController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Area("Admin")]
        public async Task<IActionResult> Index()
        {
            TempData["menu"] = "Invoice";
            return View();
        }

        [Area("Admin")]
        public async Task<IActionResult> Create(InvoiceDto? _InvoiceDto)
        {
            // Khởi tạo Menu (bắt buộc)
            TempData["menu"] = "Invoice";

            IEnumerable<InvoiceTypeDto> InvoiceTypes = await _unitOfWork.InvoiceType.GetAllInvoiceType();
            InvoiceTypes = InvoiceTypes.ToList();
            ViewData["InvoiceType"] = new SelectList(InvoiceTypes, "Id", "InvoiceTypeName");

            // lấy value cho Cbb PromotionDto

			IEnumerable<ServiceTypeDto> serviceTypes = await _unitOfWork.ServiceType.GetAllServiceType();
			serviceTypes = serviceTypes.ToList();
			ViewData["serviceType"] = new SelectList(serviceTypes, "Id", "TypeName");
			IEnumerable<ProductTypeDto> productType = await _unitOfWork.ProductType.GetAllProductType();
			productType = productType.ToList();
			ViewData["productType"] = new SelectList(productType, "Id", "ProductTypeName");
			IEnumerable<PromotionDto> promotions = await _unitOfWork.Promotion.GetAllPromotion();
			promotions = promotions.ToList();
			ViewData["promotion"] = new SelectList(promotions, "Id", "PromotionName");

			// InvoiceDto hợp lệ
			if (ModelState.IsValid)
            {
                // Add data in DB
                var n = await _unitOfWork.Invoice.CreateInvoice(_InvoiceDto);
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
                    await _unitOfWork.Action.CreateAction(IdUser, 1, "Thêm mới dữ liệu Hóa Đơn {" + _InvoiceDto.Id + "}");
                    TempData["success"] = "Thêm mới thông tin thành công";
                    return RedirectToAction("Index");
                }
                // Add status is error
                else
                {
                    TempData["error"] = "Thêm mới thông tin thất bại";
                }
            }
            // InvoiceDto không hợp lệ
            return View();
        }

        [Area("Admin")]
        public async Task<IActionResult> Detail(int? id = 1)
        {
            var n = await _unitOfWork.Invoice.GetInvoice((int)id);
            IEnumerable<InvoiceTypeDto> InvoiceTypes = await _unitOfWork.InvoiceType.GetAllInvoiceType();
            InvoiceTypes = InvoiceTypes.ToList();
            ViewData["InvoiceType"] = new SelectList(InvoiceTypes, "Id", "InvoiceTypeName", n.InvoiceTypeId);

            // lấy value cho Cbb PromotionDto
            IEnumerable<UserDto> User = await _unitOfWork.User.GetAllUser();
            User = User.ToList();
            ViewData["promotion"] = new SelectList(User, "Id", "FullName", n.CreatedByUserId);
            // Khởi tạo Menu (bắt buộc)
            TempData["menu"] = "Invoice";
            TempData["EditID"] = id;
            // Get Invoice in DB
            
            return View(n);
        }

        [Area("Admin")]
        public async Task<IActionResult> Edit(InvoiceDto? _InvoiceDto)
        {
            // _InvoiceDto hợp lệ
            if (ModelState.IsValid)
            {
                // Update data in Db
                var n = await _unitOfWork.Invoice.UpdateInvoice(_InvoiceDto);
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
                    await _unitOfWork.Action.CreateAction(IdUser, 2, "Thay đổi dữ liệu Hóa Đơn {" + _InvoiceDto.Id + "}");
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
            // _InvoiceDto không hợp lệ
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
                        var n = await _unitOfWork.Invoice.GetInvoice(id);
                        dl = await _unitOfWork.Invoice.DeleteInvoice(n);
                    //Tự động Thêm Hitory action
                    int IdUser = 0;
                    if (Request.Cookies.TryGetValue("IdUser", out string intString))
                    {
                        if (int.TryParse(intString, out int myIntValue))
                        {
                            IdUser = myIntValue;
                        }
                    }
                    await _unitOfWork.Action.CreateAction(IdUser, 3, "Xóa dữ liệu Hóa Đơn {" + n.Id + "}");
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
            // Trong một action method hoặc Invoice
            List<InvoiceDto> list = await _unitOfWork.Invoice.GetAllInvoice() as List<InvoiceDto>;

            string json = JsonConvert.SerializeObject(list);

            // Sử dụng biến json như bạn muốn, ví dụ:
            return Content(json, "application/json");
        }

		[Area("Admin")]
		[HttpPost]
		public async Task<IActionResult> GetALLServiceByType(int id)
		{
			// Trong một action method hoặc Invoice
			List<ServiceDto> list = await _unitOfWork.Service.GetAllServiceByType(id) as List<ServiceDto>;
			string json = JsonConvert.SerializeObject(list);
			// Sử dụng biến json như bạn muốn, ví dụ:
			return Content(json, "application/json");
		}

		[Area("Admin")]
		[HttpPost]
		public async Task<IActionResult> GetService(int id)
		{
			// Trong một action method hoặc Invoice
			ServiceDto list = await _unitOfWork.Service.GetService(id) as ServiceDto;
			string json = JsonConvert.SerializeObject(list);
			// Sử dụng biến json như bạn muốn, ví dụ:
			return Content(json, "application/json");
		}

		[Area("Admin")]
		[HttpPost]
		public async Task<IActionResult> GetCustomerBySDT(string sdt)
		{
			// Trong một action method hoặc CustomerFollowUp
			CustomerDto n = await _unitOfWork.Customer.GetCustomerBySDT(sdt);
			string json = JsonConvert.SerializeObject(n);
			// Sử dụng biến json như bạn muốn, ví dụ:
			return Content(json, "application/json");
		}

		[Area("Admin")]
		[HttpPost]
		public async Task<IActionResult> GetALLProductByType(int id)
		{
			// Trong một action method hoặc Invoice
			List<ProductDto> list = await _unitOfWork.Product.GetAllProductByType(id) as List<ProductDto>;

			string json = JsonConvert.SerializeObject(list);

			// Sử dụng biến json như bạn muốn, ví dụ:
			return Content(json, "application/json");
		}

		[Area("Admin")]
		[HttpPost]
		public async Task<IActionResult> GetProduct(int id)
		{
			// Trong một action method hoặc Invoice
			ProductDto list = await _unitOfWork.Product.GetProduct(id);

			string json = JsonConvert.SerializeObject(list);

			// Sử dụng biến json như bạn muốn, ví dụ:
			return Content(json, "application/json");
		}

		[Area("Admin")]
        [HttpPost]
        public async Task<IActionResult> GetInvoiceType(int id)
        {
            // Trong một action method hoặc Invoice
            InvoiceTypeDto n = await _unitOfWork.InvoiceType.GetInvoiceType(id);
            string json = JsonConvert.SerializeObject(n);
            // Sử dụng biến json như bạn muốn, ví dụ:
            return Content(json, "application/json");
        }

		[Area("Admin")]
		[HttpPost]
		public async Task<IActionResult> GetPromotion(int id)
		{
			// Trong một action method hoặc Invoice
			PromotionDto n = await _unitOfWork.Promotion.GetPromotion(id);
			string json = JsonConvert.SerializeObject(n);
			// Sử dụng biến json như bạn muốn, ví dụ:
			return Content(json, "application/json");
		}

		
        [HttpPost]
        public async Task<IActionResult> GetUser(int id)
        {
            // Trong một action method hoặc Invoice
            UserDto n = await _unitOfWork.User.GetUser(id);
            string json = JsonConvert.SerializeObject(n);
            // Sử dụng biến json như bạn muốn, ví dụ:
            return Content(json, "application/json");
        }

		[Area("Admin")]
		[HttpPost]
		public async Task<IActionResult> CreateInvoiceDetail(List<InvoiceDetailDto> invoiceDetailsArray, InvoiceDto invoice)
		{


			foreach (var detail in invoiceDetailsArray)
			{
				// Sử dụng detail ở đây để thực hiện các thao tác cần thiết
			}
			string json = JsonConvert.SerializeObject(true);
			return Content(json, "application/json");
		}
	}
}
