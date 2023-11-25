using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using WebThamMyVien.Interfaces;
using WebThamMyVien.Models;

namespace WebThamMyVien.Areas.Admin.Controllers
{
    public class UserAccountController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserAccountController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Area("Admin")]
        public async Task<IActionResult> Index()
        {
            TempData["menu"] = "UserAccount";
            return View();
        }
        [Area("Admin")]
        public async Task<IActionResult> Detail(int? id = 1)
        {
            var n = await _unitOfWork.UserAccount.GetUserAccount((int)id);
            // lấy value cho 2 Cbb 
            IEnumerable<UserStatusDto> UserStatusDtos = await _unitOfWork.UserStatus.GetAllUserStatus();
            UserStatusDtos = UserStatusDtos.ToList();
            ViewData["UserStatusDtos"] = new SelectList(UserStatusDtos, "Id", "StatusName", n.UserStatusId);

            IEnumerable<AccountTypeDto> accountTypeDto = await _unitOfWork.AccountType.GetAllAccountType();
            accountTypeDto = accountTypeDto.Skip(1).ToList();
            ViewData["accountTypeDto"] = new SelectList(accountTypeDto, "Id", "AccountTypeName", n.AccountTypeId);
            // Khởi tạo Menu (bắt buộc)
            if (id == 1)
            {
                ViewData["UserName"] = "Admin";
            }
            else
            {
                UserDto us = await _unitOfWork.User.GetUser((int)n.UserId);
                ViewData["UserName"] = us.FullName;
            }

            TempData["menu"] = "UserAccount";
            TempData["EditID"] = id;
            // Get UserAccount in DB

            return View(n);
        }

        [Area("Admin")]
        public async Task<IActionResult> Edit(UserAccountDto? _UserAccountDto)
        {
            // _UserAccountDto hợp lệ
            if (ModelState.IsValid)
            {
                // Update data in Db
                var n = await _unitOfWork.UserAccount.UpdateUserAccount(_UserAccountDto);
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
                    await _unitOfWork.Action.CreateAction(IdUser, 2, "Thay đổi dữ liệu Tài Khoản {" + _UserAccountDto.Username + "}");
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
            // _UserAccountDto không hợp lệ
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
                    var n = await _unitOfWork.UserAccount.GetUserAccount(id);
                    dl = await _unitOfWork.UserAccount.DeleteUserAccount(n);
                    //Tự động Thêm Hitory action
                    int IdUser = 0;
                    if (Request.Cookies.TryGetValue("IdUser", out string intString))
                    {
                        if (int.TryParse(intString, out int myIntValue))
                        {
                            IdUser = myIntValue;
                        }
                    }
                    await _unitOfWork.Action.CreateAction(IdUser, 1, "Xóa dữ liệu Tài Khoản {" + n.Username + "}");
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
            // Trong một action method hoặc UserAccount
            List<UserAccountDto> list = await _unitOfWork.UserAccount.GetAllUserAccount() as List<UserAccountDto>;

            string json = JsonConvert.SerializeObject(list);

            // Sử dụng biến json như bạn muốn, ví dụ:
            return Content(json, "application/json");
        }

        [Area("Admin")]
        [HttpPost]
        public async Task<IActionResult> GetUserStatus(int id)
        {
            // Trong một action method hoặc UserAccount
            UserStatusDto n = await _unitOfWork.UserStatus.GetUserStatus(id);
            string json = JsonConvert.SerializeObject(n);
            // Sử dụng biến json như bạn muốn, ví dụ:
            return Content(json, "application/json");
        }
        [Area("Admin")]
        [HttpPost]
        public async Task<IActionResult> GetUserById(int id)
        {
            // Trong một action method hoặc UserAccount
            UserDto n = await _unitOfWork.User.GetUser(id);
            string json = JsonConvert.SerializeObject(n);
            // Sử dụng biến json như bạn muốn, ví dụ:
            return Content(json, "application/json");
        }
        [Area("Admin")]
        [HttpPost]
        public async Task<IActionResult> GetAccountType(int id)
        {
            // Trong một action method hoặc UserAccount
            AccountTypeDto n = await _unitOfWork.AccountType.GetAccountType(id);
            string json = JsonConvert.SerializeObject(n);
            // Sử dụng biến json như bạn muốn, ví dụ:
            return Content(json, "application/json");
        }
    }
}
