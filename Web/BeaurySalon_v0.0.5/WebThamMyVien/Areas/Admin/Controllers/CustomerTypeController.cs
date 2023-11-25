using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebThamMyVien.Interfaces;
using WebThamMyVien.Models;

namespace WebThamMyVien.Areas.Admin.Controllers
{
    public class CustomerTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CustomerTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Area("Admin")]
        public async Task<IActionResult> Index()
        {

            TempData["menu"] = "CustomerType";
            return View();
        }

        [Area("Admin")]
        public async Task<IActionResult> Create(CustomerTypeDto? value)
        {
            // Khởi tạo Menu (bắt buộc)
            TempData["menu"] = "CustomerType";
            // CustomerTypeDto hợp lệ
            if (ModelState.IsValid)
            {

                // Add data in DB
                var n = await _unitOfWork.CustomerType.CreateCustomerType(value);
                // Add status is success
                if (n)
                {
                    int IdUser = 0;
                    if (Request.Cookies.TryGetValue("IdUser", out string intString))
                    {
                        if (int.TryParse(intString, out int myIntValue))
                        {
                            IdUser = myIntValue;
                        }
                    }
                    await _unitOfWork.Action.CreateAction(IdUser, 1, "Thêm mới dữ liệu Loại Khách Hàng {" + value.TypeName + "}");
                    TempData["success"] = "Thêm mới thông tin thành công";
                    return RedirectToAction("Index");
                }
                // Add status is error
                else
                {
                    TempData["error"] = "Thêm mới thông tin thất bại";
                }
            }
            // CustomerTypeDto không hợp lệ
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
            TempData["menu"] = "CustomerType";
            TempData["EditCustomerType"] = id;
            // Get CustomerType in DB
            var n = await _unitOfWork.CustomerType.GetCustomerType((int)id);
            return View(n);
        }

        [Area("Admin")]
        public async Task<IActionResult> Edit(CustomerTypeDto? value)
        {
            // CustomerTypeDto hợp lệ
            if (ModelState.IsValid)
            {
                // Update data in Db
                var n = await _unitOfWork.CustomerType.UpdateCustomerType(value);
                // Update status is success
                if (n)
                {
                    int IdUser = 0;
                    if (Request.Cookies.TryGetValue("IdUser", out string intString))
                    {
                        if (int.TryParse(intString, out int myIntValue))
                        {
                            IdUser = myIntValue;
                        }
                    }
                    await _unitOfWork.Action.CreateAction(IdUser, 2, "Thay đổi dữ liệu Loại Khách Hàng {" + value.TypeName + "}");
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
            // CustomerTypeDto không hợp lệ
            else
            {
                TempData["warning"] = "Thông tin không hợp lệ";
            }

            return RedirectToAction("Detail", new { id = TempData["EditCustomerType"] });
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
                var listA = await _unitOfWork.Customer.GetAllCustomer();
                foreach (var id in ids)
                {
                    int dll = 0;
                    foreach (var item in listA)
                    {
                        if (item.CustomerTypeId == id)
                        {
                            dll += 1;
                            break;
                        }
                    }
                    if (dll == 0)
                    {
                        var n = await _unitOfWork.CustomerType.GetCustomerType(id);
                        dl = await _unitOfWork.CustomerType.DeleteCustomerType(n);
                        int IdUser = 0;
                        if (Request.Cookies.TryGetValue("IdUser", out string intString))
                        {
                            if (int.TryParse(intString, out int myIntValue))
                            {
                                IdUser = myIntValue;
                            }
                        }
                        await _unitOfWork.Action.CreateAction(IdUser, 3, "Xóa dữ liệu Loại Khách Hàng {" + n.TypeName + "}");
                    }
                    else
                    {
                        var n = await _unitOfWork.CustomerType.GetCustomerType(id);
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
            List<CustomerTypeDto> list = await _unitOfWork.CustomerType.GetAllCustomerType() as List<CustomerTypeDto>;

            string json = JsonConvert.SerializeObject(list);

            // Sử dụng biến json như bạn muốn, ví dụ:
            return Content(json, "application/json");
        }
    }
}
