using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using WebThamMyVien.Interfaces;
using WebThamMyVien.Models;

namespace WebThamMyVien.Areas.Admin.Controllers
{
    public class UserAccountController : Controller
    {
        private readonly IUserAccountRepository _UserAccountRepository;
        private readonly IUserStatusRepository _UserStatusRepository;
        private readonly IAccountTypeRepository _AccountTypeRepository;
        private readonly IUserRepository _UserRepository;
        public UserAccountController(IUserAccountRepository UserAccountRepository, IUserRepository UserRepository, IUserStatusRepository UserStatusRepository, IAccountTypeRepository AccountTypeRepository)
        {
            _UserAccountRepository = UserAccountRepository;
            _UserStatusRepository = UserStatusRepository;
            _AccountTypeRepository = AccountTypeRepository;
            _UserRepository = UserRepository;
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
            var n = await _UserAccountRepository.GetUserAccount((int)id);
            // lấy value cho 2 Cbb 
            IEnumerable<UserStatusDto> UserStatusDtos = await _UserStatusRepository.GetAllUserStatus();
            UserStatusDtos = UserStatusDtos.ToList();
            ViewData["UserStatusDtos"] = new SelectList(UserStatusDtos, "Id", "StatusName", n.UserStatusId);

            IEnumerable<AccountTypeDto> accountTypeDto = await _AccountTypeRepository.GetAllAccountType();
            accountTypeDto = accountTypeDto.Skip(1).ToList();
            ViewData["accountTypeDto"] = new SelectList(accountTypeDto, "Id", "AccountTypeName", n.AccountTypeId);
            // Khởi tạo Menu (bắt buộc)
            if (id == 1)
            {
                ViewData["UserName"] = "Admin";
            }
            else
            {
                UserDto us = await _UserRepository.GetUser((int)n.UserId);
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
                var n = await _UserAccountRepository.UpdateUserAccount(_UserAccountDto);
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
                    var n = await _UserAccountRepository.GetUserAccount(id);
                    dl = await _UserAccountRepository.DeleteUserAccount(n);
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
            List<UserAccountDto> list = await _UserAccountRepository.GetAllUserAccount() as List<UserAccountDto>;

            string json = JsonConvert.SerializeObject(list);

            // Sử dụng biến json như bạn muốn, ví dụ:
            return Content(json, "application/json");
        }

        [Area("Admin")]
        [HttpPost]
        public async Task<IActionResult> GetUserStatus(int id)
        {
            // Trong một action method hoặc UserAccount
            UserStatusDto n = await _UserStatusRepository.GetUserStatus(id);
            string json = JsonConvert.SerializeObject(n);
            // Sử dụng biến json như bạn muốn, ví dụ:
            return Content(json, "application/json");
        }
        [Area("Admin")]
        [HttpPost]
        public async Task<IActionResult> GetUserById(int id)
        {
            // Trong một action method hoặc UserAccount
            UserDto n = await _UserRepository.GetUser(id);
            string json = JsonConvert.SerializeObject(n);
            // Sử dụng biến json như bạn muốn, ví dụ:
            return Content(json, "application/json");
        }
        [Area("Admin")]
        [HttpPost]
        public async Task<IActionResult> GetAccountType(int id)
        {
            // Trong một action method hoặc UserAccount
            AccountTypeDto n = await _AccountTypeRepository.GetAccountType(id);
            string json = JsonConvert.SerializeObject(n);
            // Sử dụng biến json như bạn muốn, ví dụ:
            return Content(json, "application/json");
        }
    }
}
