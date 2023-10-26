using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using WebThamMyVien.Interfaces;
using WebThamMyVien.Models;

namespace WebThamMyVien.Areas.Admin.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CustomerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Area("Admin")]
        public async Task<IActionResult> Index()
        {
            TempData["menu"] = "Customer";
            return View();
        }

        [Area("Admin")]
        public async Task<IActionResult> Create(CustomerDto? _CustomerDto)
        {
            // Khởi tạo Menu (bắt buộc)
            TempData["menu"] = "Customer";

            IEnumerable<CustomerTypeDto> CustomerTypes = await _unitOfWork.CustomerType.GetAllCustomerType();
            CustomerTypes = CustomerTypes.ToList();
            ViewData["CustomerType"] = new SelectList(CustomerTypes, "Id", "TypeName");

            IEnumerable<CustomerStatusDto> CustomerStatuss = await _unitOfWork.CustomerStatus.GetAllCustomerStatus();
            CustomerStatuss = CustomerStatuss.ToList();
            ViewData["CustomerStatus"] = new SelectList(CustomerStatuss, "Id", "StatusName");

            // CustomerDto hợp lệ
            if (ModelState.IsValid)
            {
                // Add data in DB
                var n = await _unitOfWork.Customer.CreateCustomer(_CustomerDto);
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
                    await _unitOfWork.Action.CreateAction(IdUser, 1, "Thêm mới dữ liệu Khách Hàng {" + _CustomerDto.FullName + "}");
                    TempData["success"] = "Thêm mới thông tin thành công";
                    return RedirectToAction("Index");
                }
                // Add status is error
                else
                {
                    TempData["error"] = "Thêm mới thông tin thất bại";
                }
            }
            // CustomerDto không hợp lệ
            else
            {
                if (_CustomerDto.FullName == "" || _CustomerDto.FullName == null)
                {
                    return View(_CustomerDto);
                }
                TempData["warning"] = "Thông tin không hợp lệ";
            }
            return View(_CustomerDto);
        }

        [Area("Admin")]
        public async Task<IActionResult> Detail(int? id = 1)
        {
            var n = await _unitOfWork.Customer.GetCustomer((int)id);
            IEnumerable<CustomerTypeDto> CustomerTypes = await _unitOfWork.CustomerType.GetAllCustomerType();
            CustomerTypes = CustomerTypes.ToList();
            ViewData["CustomerType"] = new SelectList(CustomerTypes, "Id", "TypeName" , n.CustomerTypeId);

            IEnumerable<CustomerStatusDto> CustomerStatuss = await _unitOfWork.CustomerStatus.GetAllCustomerStatus();
            CustomerStatuss = CustomerStatuss.ToList();
            ViewData["CustomerStatus"] = new SelectList(CustomerStatuss, "Id", "StatusName", n.CustomerStatusId);

            // Khởi tạo Menu (bắt buộc)
            TempData["menu"] = "Customer";
            TempData["EditID"] = id;
            // Get Customer in DB

            return View(n);
        }

        [Area("Admin")]
        public async Task<IActionResult> Edit(CustomerDto? _CustomerDto)
        {
            // _CustomerDto hợp lệ
            if (ModelState.IsValid)
            {
                // Update data in Db
                var n = await _unitOfWork.Customer.UpdateCustomer(_CustomerDto);
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
                    await _unitOfWork.Action.CreateAction(IdUser, 2, "Thay đổi dữ liệu Khách Hàng {" + _CustomerDto.FullName + "}");
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
            // _CustomerDto không hợp lệ
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
                    var n = await _unitOfWork.Customer.GetCustomer(id);
                    dl = await _unitOfWork.Customer.DeleteCustomer(n);
                    //Tự động Thêm Hitory action
                    int IdUser = 0;
                    if (Request.Cookies.TryGetValue("IdUser", out string intString))
                    {
                        if (int.TryParse(intString, out int myIntValue))
                        {
                            IdUser = myIntValue;
                        }
                    }
                    await _unitOfWork.Action.CreateAction(IdUser, 3, "Xóa dữ liệu Khách Hàng {" + n.FullName + "}");
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
            // Trong một action method hoặc Customer
            List<CustomerDto> list = await _unitOfWork.Customer.GetAllCustomer() as List<CustomerDto>;

            string json = JsonConvert.SerializeObject(list);

            // Sử dụng biến json như bạn muốn, ví dụ:
            return Content(json, "application/json");
        }

        [Area("Admin")]
        [HttpPost]
        public async Task<IActionResult> GetCustomerType(int id)
        {
            CustomerTypeDto n = await _unitOfWork.CustomerType.GetCustomerType(id);
            string json = JsonConvert.SerializeObject(n);
            return Content(json, "application/json");
        }

        [Area("Admin")]
        [HttpPost]
        public async Task<IActionResult> GetCustomerStatus(int id)
        {
            CustomerStatusDto n = await _unitOfWork.CustomerStatus.GetCustomerStatus(id);
            string json = JsonConvert.SerializeObject(n);
            return Content(json, "application/json");
        }
    }
}
