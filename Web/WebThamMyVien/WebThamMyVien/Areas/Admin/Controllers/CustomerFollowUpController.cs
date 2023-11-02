using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using WebThamMyVien.Interfaces;
using WebThamMyVien.Models;

namespace WebThamMyVien.Areas.Admin.Controllers
{
    public class CustomerFollowUpController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CustomerFollowUpController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Area("Admin")]
        public async Task<IActionResult> Index()
        {
            TempData["menu"] = "CustomerFollowUp";
            return View();
        }

        [Area("Admin")]
        public async Task<IActionResult> Create(CustomerFollowUpDto? _CustomerFollowUpDto)
        {
            // Khởi tạo Menu (bắt buộc)
            TempData["menu"] = "CustomerFollowUp";

            IEnumerable<FollowUpStatusDto> FollowUpStatus = await _unitOfWork.FollowUpStatus.GetAllFollowUpStatus();
            FollowUpStatus = FollowUpStatus.ToList();
            ViewData["FollowUpStatus"] = new SelectList(FollowUpStatus, "Id", "StatusName");

            // lấy value cho Cbb 
            IEnumerable<BranchDto> Branch = await _unitOfWork.Branch.GetAllBranch();
            Branch = Branch.ToList();
            ViewData["Branch"] = new SelectList(Branch, "Id", "Name");

            // lấy value cho Cbb 
            IEnumerable<UserDto> User = await _unitOfWork.User.GetAllUser();
            User = User.ToList();
            ViewData["User"] = new SelectList(User, "Id", "FullName");


            // CustomerFollowUpDto hợp lệ
            if (ModelState.IsValid)
            {
                // Add data in DB
                var n = await _unitOfWork.CustomerFollowUp.CreateCustomerFollowUp(_CustomerFollowUpDto);
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
                    await _unitOfWork.Action.CreateAction(IdUser, 1, "Thêm mới dữ liệu Chăm Khách {" + _CustomerFollowUpDto.CustomerId + "}");
                    TempData["success"] = "Thêm mới thông tin thành công";
                    return RedirectToAction("Index");
                }
                // Add status is error
                else
                {
                    TempData["error"] = "Thêm mới thông tin thất bại";
                }
            }
            // CustomerFollowUpDto không hợp lệ
            else
            {
                if (_CustomerFollowUpDto.CustomerId == 0|| _CustomerFollowUpDto.CustomerId == null)
                {
                    return View(_CustomerFollowUpDto);
                }
                TempData["warning"] = "Thông tin không hợp lệ";
            }
            return View(_CustomerFollowUpDto);
        }

        [Area("Admin")]
        public async Task<IActionResult> Detail(int? id = 1)
        {
            var n = await _unitOfWork.CustomerFollowUp.GetCustomerFollowUp((int)id);
            IEnumerable<FollowUpStatusDto> FollowUpStatus = await _unitOfWork.FollowUpStatus.GetAllFollowUpStatus();
            FollowUpStatus = FollowUpStatus.ToList();
            ViewData["FollowUpStatus"] = new SelectList(FollowUpStatus, "Id", "StatusName");

            // lấy value cho Cbb 
            IEnumerable<BranchDto> Branch = await _unitOfWork.Branch.GetAllBranch();
            Branch = Branch.ToList();
            ViewData["Branch"] = new SelectList(Branch, "Id", "Name");

            // lấy value cho Cbb 
            IEnumerable<UserDto> User = await _unitOfWork.User.GetAllUser();
            User = User.ToList();
            ViewData["User"] = new SelectList(User, "Id", "FullName");
            // Khởi tạo Menu (bắt buộc)
            TempData["menu"] = "CustomerFollowUp";
            TempData["EditID"] = id;
            // Get CustomerFollowUp in DB

            return View(n);
        }

        [Area("Admin")]
        public async Task<IActionResult> Edit(CustomerFollowUpDto? _CustomerFollowUpDto)
        {
            // _CustomerFollowUpDto hợp lệ
            if (ModelState.IsValid)
            {
                // Update data in Db
                var n = await _unitOfWork.CustomerFollowUp.UpdateCustomerFollowUp(_CustomerFollowUpDto);
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
                    await _unitOfWork.Action.CreateAction(IdUser, 2, "Thay đổi dữ liệu Chăm Khách {" + _CustomerFollowUpDto.CustomerId + "}");
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
            // _CustomerFollowUpDto không hợp lệ
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
                    var n = await _unitOfWork.CustomerFollowUp.GetCustomerFollowUp(id);
                    dl = await _unitOfWork.CustomerFollowUp.DeleteCustomerFollowUp(n);
                    //Tự động Thêm Hitory action
                    int IdUser = 0;
                    if (Request.Cookies.TryGetValue("IdUser", out string intString))
                    {
                        if (int.TryParse(intString, out int myIntValue))
                        {
                            IdUser = myIntValue;
                        }
                    }
                    await _unitOfWork.Action.CreateAction(IdUser, 3, "Xóa dữ liệu Chăm Khách {" + n.CustomerId + "}");
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
            // Trong một action method hoặc CustomerFollowUp
            List<CustomerFollowUpDto> list = await _unitOfWork.CustomerFollowUp.GetAllCustomerFollowUp() as List<CustomerFollowUpDto>;

            string json = JsonConvert.SerializeObject(list);

            // Sử dụng biến json như bạn muốn, ví dụ:
            return Content(json, "application/json");
        }

        [Area("Admin")]
        [HttpPost]
        public async Task<IActionResult> GetFollowUpStatus(int id)
        {
            // Trong một action method hoặc CustomerFollowUp
            FollowUpStatusDto n = await _unitOfWork.FollowUpStatus.GetFollowUpStatus(id);
            string json = JsonConvert.SerializeObject(n);
            // Sử dụng biến json như bạn muốn, ví dụ:
            return Content(json, "application/json");
        }

        [Area("Admin")]
        [HttpPost]
        public async Task<IActionResult> GetUser(int id)
        {
            // Trong một action method hoặc CustomerFollowUp
            UserDto n = await _unitOfWork.User.GetUser(id);
            string json = JsonConvert.SerializeObject(n);
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
        public async Task<IActionResult> GetBranch(int id)
        {
            // Trong một action method hoặc CustomerFollowUp
            BranchDto n = await _unitOfWork.Branch.GetBranch(id);
            string json = JsonConvert.SerializeObject(n);
            // Sử dụng biến json như bạn muốn, ví dụ:
            return Content(json, "application/json");
        }

    }
}
