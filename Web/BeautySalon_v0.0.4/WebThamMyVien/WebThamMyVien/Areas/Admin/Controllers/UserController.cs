using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using WebThamMyVien.Interfaces;
using WebThamMyVien.Models;
using WebThamMyVien.Repository;

namespace WebThamMyVien.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _UserRepository;
        private readonly IUserTypeRepository _UserTypeRepository;
        private readonly IUserStatusRepository _UserStatusRepository;
        private readonly IBranchRepository _BranchRepository;
        private readonly IUserAccountRepository _UserAccountRepository;

        public UserController(IUserRepository UserRepository, IUserTypeRepository UserTypeRepository, IUserStatusRepository UserStatusRepository, IBranchRepository BranchRepository, IUserAccountRepository UserAccountRepository)
        {
            _UserRepository = UserRepository;
            _UserTypeRepository = UserTypeRepository;
            _UserStatusRepository = UserStatusRepository;
            _BranchRepository = BranchRepository;
            _UserAccountRepository = UserAccountRepository;
        }

        [Area("Admin")]
        public async Task<IActionResult> Index()
        {
            TempData["menu"] = "User";
            return View();
        }

        [Area("Admin")]
        public async Task<IActionResult> Create(UserDto? _UserDto)
        {
            // Khởi tạo Menu (bắt buộc)
            TempData["menu"] = "User";
            // lấy value cho 2 Cbb 
            IEnumerable<UserTypeDto> userTypes = await _UserTypeRepository.GetAllUserType();
            userTypes = userTypes.ToList();
            ViewData["userTypes"] = new SelectList(userTypes, "Id", "Name");

            IEnumerable<UserStatusDto> userStatusDtos = await _UserStatusRepository.GetAllUserStatus();
            userStatusDtos = userStatusDtos.ToList();
            ViewData["userStatusDtos"] = new SelectList(userStatusDtos, "Id", "StatusName");

            IEnumerable<BranchDto> branchs = await _BranchRepository.GetAllBranch();
            branchs = branchs.ToList();
            ViewData["branchs"] = new SelectList(branchs, "Id", "Name");
            // UserDto hợp lệ
            if (ModelState.IsValid)
            {
                // Add data in DB
                var n = await _UserRepository.CreateUser(_UserDto);
                // Add status is success
                if (n)
                {
                    TempData["success"] = "Thêm mới thông tin thành công";
                    //UserAccountDto us = new UserAccountDto();
                    //us.Username = _UserDto.Email;
                    //us.Id = 0;
                    //us.Password = _UserDto.Email;
                    //us.Email = _UserDto.Email;
                    //us.UserStatusId = _UserDto.UserStatusId;
                    //us.UserId = _UserDto.Id;
                    //us.AccountTypeId = 3;
                    //var st = await _UserAccountRepository.CreateUserAccount(us);
                    return RedirectToAction("Index");
                }
                // Add status is error
                else
                {
                    TempData["error"] = "Thêm mới thông tin thất bại";
                }
            }
            // UserDto không hợp lệ
            else
            {
                if (_UserDto.FullName == "" || _UserDto.FullName == null)
                {
                    return View(_UserDto);
                }
                TempData["warning"] = "Thông tin không hợp lệ";
            }
            return View(_UserDto);
        }

        [Area("Admin")]
        public async Task<IActionResult> Detail(int? id = 1)
        {
            var n = await _UserRepository.GetUser((int)id);
            // lấy value cho 2 Cbb 
            IEnumerable<UserTypeDto> userTypes = await _UserTypeRepository.GetAllUserType();
            userTypes = userTypes.ToList();
            ViewData["userTypes"] = new SelectList(userTypes, "Id", "Name", n.UserTypeId);

            IEnumerable<UserStatusDto> userStatusDtos = await _UserStatusRepository.GetAllUserStatus();
            userStatusDtos = userStatusDtos.ToList();
            ViewData["userStatusDtos"] = new SelectList(userStatusDtos, "Id", "StatusName", n.UserStatusId);

            IEnumerable<BranchDto> branchs = await _BranchRepository.GetAllBranch();
            branchs = branchs.ToList();
            ViewData["branchs"] = new SelectList(branchs, "Id", "Name", n.BranchId);
            // Khởi tạo Menu (bắt buộc)
            TempData["menu"] = "User";
            TempData["EditID"] = id;
            // Get User in DB

            return View(n);
        }

        [Area("Admin")]
        public async Task<IActionResult> Edit(UserDto? _UserDto)
        {
            // _UserDto hợp lệ
            if (ModelState.IsValid)
            {
                // Update data in Db
                var n = await _UserRepository.UpdateUser(_UserDto);
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
            // _UserDto không hợp lệ
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
                    var n = await _UserRepository.GetUser(id);
                    dl = await _UserRepository.DeleteUser(n);
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
            // Trong một action method hoặc User
            List<UserDto> list = await _UserRepository.GetAllUser() as List<UserDto>;

            string json = JsonConvert.SerializeObject(list);

            // Sử dụng biến json như bạn muốn, ví dụ:
            return Content(json, "application/json");
        }

        [Area("Admin")]
        [HttpPost]
        public async Task<IActionResult> GetUserType(int id)
        {
            // Trong một action method hoặc User
            UserTypeDto n = await _UserTypeRepository.GetUserType(id);
            string json = JsonConvert.SerializeObject(n);
            // Sử dụng biến json như bạn muốn, ví dụ:
            return Content(json, "application/json");
        }

        [Area("Admin")]
        [HttpPost]
        public async Task<IActionResult> GetUserStatus(int id)
        {
            // Trong một action method hoặc User
            UserStatusDto n = await _UserStatusRepository.GetUserStatus(id);
            string json = JsonConvert.SerializeObject(n);
            // Sử dụng biến json như bạn muốn, ví dụ:
            return Content(json, "application/json");
        }

        [Area("Admin")]
        [HttpPost]
        public async Task<IActionResult> GetBranch(int id)
        {
            // Trong một action method hoặc User
            BranchDto n = await _BranchRepository.GetBranch(id);
            string json = JsonConvert.SerializeObject(n);
            // Sử dụng biến json như bạn muốn, ví dụ:
            return Content(json, "application/json");
        }
    }
}
