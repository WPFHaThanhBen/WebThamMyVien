using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebThamMyVien.Interfaces;
using WebThamMyVien.Models;

namespace WebThamMyVien.Areas.Admin.Controllers
{
    public class AppointmentStatusController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public AppointmentStatusController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Area("Admin")]
        public async Task<IActionResult> Index()
        {

            TempData["menu"] = "AppointmentStatus";
            return View();
        }

        [Area("Admin")]
        public async Task<IActionResult> Create(AppointmentStatusDto? value)
        {
            // Khởi tạo Menu (bắt buộc)
            TempData["menu"] = "AppointmentStatus";
            // AppointmentStatusDto hợp lệ
            if (ModelState.IsValid)
            {

                // Add data in DB
                var n = await _unitOfWork.AppointmentStatus.CreateAppointmentStatus(value);
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
                    await _unitOfWork.Action.CreateAction(IdUser, 1, "Thêm mới dữ liệu Trạng Thái Lịch Hẹn {" + value.StatusName + "}");
                    TempData["success"] = "Thêm mới thông tin thành công";
                    return RedirectToAction("Index");
                }
                // Add status is error
                else
                {
                    TempData["error"] = "Thêm mới thông tin thất bại";
                }
            }
            // AppointmentStatusDto không hợp lệ
            else
            {
                if (value.StatusName == "" || value.StatusName == null)
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
            TempData["menu"] = "AppointmentStatus";
            TempData["EditAppointmentStatus"] = id;
            // Get AppointmentStatus in DB
            var n = await _unitOfWork.AppointmentStatus.GetAppointmentStatus((int)id);
            return View(n);
        }

        [Area("Admin")]
        public async Task<IActionResult> Edit(AppointmentStatusDto? value)
        {
            // AppointmentStatusDto hợp lệ
            if (ModelState.IsValid)
            {
                // Update data in Db
                var n = await _unitOfWork.AppointmentStatus.UpdateAppointmentStatus(value);
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
                    await _unitOfWork.Action.CreateAction(IdUser, 2, "Thay đổi dữ liệu Trạng Thái Lịch Hẹn {" + value.StatusName + "}");
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
            // AppointmentStatusDto không hợp lệ
            else
            {
                TempData["warning"] = "Thông tin không hợp lệ";
            }

            return RedirectToAction("Detail", new { id = TempData["EditAppointmentStatus"] });
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
                var listA = await _unitOfWork.Appointment.GetAllAppointment();
                foreach (var id in ids)
                {
                    int dll = 0;
                    foreach (var item in listA)
                    {
                        if (item.AppointmentStatusId == id)
                        {
                            dll += 1;
                            break;
                        }
                    }
                    if (dll == 0)
                    {
                        var n = await _unitOfWork.AppointmentStatus.GetAppointmentStatus(id);
                        dl = await _unitOfWork.AppointmentStatus.DeleteAppointmentStatus(n);
                        int IdUser = 0;
                        if (Request.Cookies.TryGetValue("IdUser", out string intString))
                        {
                            if (int.TryParse(intString, out int myIntValue))
                            {
                                IdUser = myIntValue;
                            }
                        }
                        await _unitOfWork.Action.CreateAction(IdUser, 3, "Xóa dữ liệu Trạng Thái Lịch Hẹn {" + n.StatusName + "}");
                    }
                    else
                    {
                        var n = await _unitOfWork.AppointmentStatus.GetAppointmentStatus(id);
                        if (_mesg == "")
                        {
                            _status = 2;
                            _mesg = "Không thể xóa thông tin " + ", " + n.StatusName;
                        }
                        else
                        {
                            _mesg += ", " + n.StatusName;
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
            List<AppointmentStatusDto> list = await _unitOfWork.AppointmentStatus.GetAllAppointmentStatus() as List<AppointmentStatusDto>;

            string json = JsonConvert.SerializeObject(list);

            // Sử dụng biến json như bạn muốn, ví dụ:
            return Content(json, "application/json");
        }
    }
}
