using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using WebThamMyVien.Interfaces;
using WebThamMyVien.Models;

namespace WebThamMyVien.Areas.Admin.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public AppointmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Area("Admin")]
        public async Task<IActionResult> Index()
        {
            TempData["menu"] = "Appointment";
            return View();
        }
        [Area("Admin")]
        public async Task<IActionResult> Create(AppointmentDto? _AppointmentDto)
        {
            // Khởi tạo Menu (bắt buộc)
            TempData["menu"] = "Appointment";

            IEnumerable<AppointmentTypeDto> AppointmentTypes = await _unitOfWork.AppointmentType.GetAllAppointmentType();
            AppointmentTypes = AppointmentTypes.ToList();
            ViewData["AppointmentType"] = new SelectList(AppointmentTypes, "Id", "TypeName");

            IEnumerable<AppointmentStatusDto> AppointmentStatus = await _unitOfWork.AppointmentStatus.GetAllAppointmentStatus();
            AppointmentStatus = AppointmentStatus.ToList();
            ViewData["AppointmentStatus"] = new SelectList(AppointmentStatus, "Id", "StatusName");

            IEnumerable<UserDto> User= await _unitOfWork.User.GetAllUser();
            User = User.ToList();
            var userList = User.Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.FullName + " (" + u.Id + ")"
            }).ToList();

            ViewData["User"] = new SelectList(userList, "Value", "Text");

            // AppointmentDto hợp lệ
            if (ModelState.IsValid)
            {
                // Add data in DB
                var n = await _unitOfWork.Appointment.CreateAppointment(_AppointmentDto);
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
                    await _unitOfWork.Action.CreateAction(IdUser, 1, "Thêm mới dữ liệu Lịch Hẹn {" + _AppointmentDto.CustomerId + "}");
                    TempData["success"] = "Thêm mới thông tin thành công";
                    return RedirectToAction("Index");
                }
                // Add status is error
                else
                {
                    TempData["error"] = "Thêm mới thông tin thất bại";
                }
            }
            return View(_AppointmentDto);
        }

        [Area("Admin")]
        public async Task<IActionResult> Detail(int? id = 1)
        {
            var n = await _unitOfWork.Appointment.GetAppointment((int)id);
            IEnumerable<AppointmentTypeDto> AppointmentTypes = await _unitOfWork.AppointmentType.GetAllAppointmentType();
            AppointmentTypes = AppointmentTypes.ToList();
            ViewData["AppointmentType"] = new SelectList(AppointmentTypes, "Id", "TypeName", n.AppointmentTypeId);

            // lấy value cho Cbb PromotionDto
            IEnumerable<AppointmentStatusDto> AppointmentStatus = await _unitOfWork.AppointmentStatus.GetAllAppointmentStatus();
            AppointmentStatus = AppointmentStatus.ToList();
            ViewData["AppointmentStatus"] = new SelectList(AppointmentStatus, "Id", "StatusName", n.AppointmentStatusId);

            IEnumerable<UserDto> User = await _unitOfWork.User.GetAllUser();
            User = User.ToList();
            var userList = User.Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.FullName + " (" + u.Id + ")"
            }).ToList();

            ViewData["User"] = new SelectList(userList, "Value", "Text");
            // Khởi tạo Menu (bắt buộc)
            TempData["menu"] = "Appointment";
            TempData["EditID"] = id;
            // Get Appointment in DB

            return View(n);
        }

        [Area("Admin")]
        public async Task<IActionResult> Edit(AppointmentDto? _AppointmentDto)
        {
            // _AppointmentDto hợp lệ
            if (ModelState.IsValid)
            {
                // Update data in Db
                var n = await _unitOfWork.Appointment.UpdateAppointment(_AppointmentDto);
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
                    await _unitOfWork.Action.CreateAction(IdUser, 2, "Thay đổi dữ liệu Lịch Hẹn {" + _AppointmentDto.CustomerId + "}");
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
            // _AppointmentDto không hợp lệ
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
                    var n = await _unitOfWork.Appointment.GetAppointment(id);
                    dl = await _unitOfWork.Appointment.DeleteAppointment(n);
                    //Tự động Thêm Hitory action
                    int IdUser = 0;
                    if (Request.Cookies.TryGetValue("IdUser", out string intString))
                    {
                        if (int.TryParse(intString, out int myIntValue))
                        {
                            IdUser = myIntValue;
                        }
                    }
                    await _unitOfWork.Action.CreateAction(IdUser, 3, "Xóa dữ liệu Lịch Hẹn {" + n.CustomerId + "}");
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
            // Trong một action method hoặc Appointment
            List<AppointmentDto> list = await _unitOfWork.Appointment.GetAllAppointment() as List<AppointmentDto>;

            string json = JsonConvert.SerializeObject(list);

            // Sử dụng biến json như bạn muốn, ví dụ:
            return Content(json, "application/json");
        }

        [Area("Admin")]
        [HttpPost]
        public async Task<IActionResult> GetAppointmentType(int id)
        {
            // Trong một action method hoặc Appointment
            AppointmentTypeDto n = await _unitOfWork.AppointmentType.GetAppointmentType(id);
            string json = JsonConvert.SerializeObject(n);
            // Sử dụng biến json như bạn muốn, ví dụ:
            return Content(json, "application/json");
        }

        [Area("Admin")]
        [HttpPost]
        public async Task<IActionResult> GetAppointmentStatus(int id)
        {
            // Trong một action method hoặc Appointment
            AppointmentStatusDto n = await _unitOfWork.AppointmentStatus.GetAppointmentStatus(id);
            string json = JsonConvert.SerializeObject(n);
            // Sử dụng biến json như bạn muốn, ví dụ:
            return Content(json, "application/json");
        }

        [Area("Admin")]
        [HttpPost]
        public async Task<IActionResult> GetCustomer(int id)
        {
            // Trong một action method hoặc Appointment
            CustomerDto n = await _unitOfWork.Customer.GetCustomer(id);
            string json = JsonConvert.SerializeObject(n);
            // Sử dụng biến json như bạn muốn, ví dụ:
            return Content(json, "application/json");
        }

        [Area("Admin")]
        [HttpPost]
        public async Task<IActionResult> GetCustomerBySDT(string sdt)
        {
            // Trong một action method hoặc Appointment
            CustomerDto n = await _unitOfWork.Customer.GetCustomerBySDT(sdt);
            string json = JsonConvert.SerializeObject(n);
            // Sử dụng biến json như bạn muốn, ví dụ:
            return Content(json, "application/json");
        }

        [Area("Admin")]
        [HttpPost]
        public async Task<IActionResult> GetUser(int id)
        {
            // Trong một action method hoặc Appointment
            UserDto n = await _unitOfWork.User.GetUser(id);
            string json = JsonConvert.SerializeObject(n);
            // Sử dụng biến json như bạn muốn, ví dụ:
            return Content(json, "application/json");
        }
    }
}
