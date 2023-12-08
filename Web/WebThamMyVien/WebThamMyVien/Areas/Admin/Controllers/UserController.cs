using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebThamMyVien.Interfaces;
using WebThamMyVien.Models;
using WebThamMyVien.Repository;

namespace WebThamMyVien.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
            IEnumerable<UserTypeDto> userTypes = await _unitOfWork.UserType.GetAllUserType();
            userTypes = userTypes.ToList();
            ViewData["userTypes"] = new SelectList(userTypes, "Id", "Name");

            IEnumerable<UserStatusDto> userStatusDtos = await _unitOfWork.UserStatus.GetAllUserStatus();
            userStatusDtos = userStatusDtos.ToList();
            ViewData["userStatusDtos"] = new SelectList(userStatusDtos, "Id", "StatusName");

            IEnumerable<BranchDto> branchs = await _unitOfWork.Branch.GetAllBranch();
            branchs = branchs.ToList();
            ViewData["branchs"] = new SelectList(branchs, "Id", "Name");
            List<UserDto> uss = (List<UserDto>)await _unitOfWork.User.GetAllUser();

            // UserDto hợp lệ
            if (ModelState.IsValid)
            {
                foreach(var i in uss)
                {
                    if(i.Email == _UserDto.Email)
                    {
                        TempData["warning"] = "Email đã được sử dụng";
                        return View(_UserDto);
                    }
                }
                // Add data in DB
                var n = await _unitOfWork.User.CreateUser(_UserDto);
                // Add status is success
                if (n)
                {
                    TempData["success"] = "Thêm mới thông tin thành công";
                    //Tự động Thêm Hitory action
                    int IdUser = 0;
                    if (Request.Cookies.TryGetValue("IdUser", out string intString))
                    {
                        if (int.TryParse(intString, out int myIntValue))
                        {
                            IdUser = myIntValue;
                        }
                    }
                    await _unitOfWork.Action.CreateAction(IdUser, 1, "Thêm mới dữ liệu Người Dùng {" + _UserDto.FullName + "}");
                    
                    //Tự động thêm Account cho user
                    List<UserDto> usss = (List<UserDto>)await _unitOfWork.User.GetAllUser();
                    UserDto lastUser = usss[usss.Count - 1];
                    UserAccountDto us = new UserAccountDto();
                    us.Id = 0;
                    us.Username = lastUser.Email;
                    us.Password = lastUser.Email;
                    us.Email = lastUser.Email;
                    us.UserStatusId = lastUser.UserStatusId;
                    us.UserId = lastUser.Id;
                    us.AccountTypeId = 2;
                    us.CustomerId = null;
                    var st = await _unitOfWork.UserAccount.CreateUserAccount(us);
                    //Tự động Thêm Hitory action
                    await _unitOfWork.Action.CreateAction(IdUser, 1, "Thêm dữ liệu Tài Khoản {" + us.Username + "}");
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
            var n = await _unitOfWork.User.GetUser((int)id);
            // lấy value cho 2 Cbb 
            IEnumerable<UserTypeDto> userTypes = await _unitOfWork.UserType.GetAllUserType();
            userTypes = userTypes.ToList();
            ViewData["userTypes"] = new SelectList(userTypes, "Id", "Name", n.UserTypeId);

            IEnumerable<UserStatusDto> userStatusDtos = await _unitOfWork.UserStatus.GetAllUserStatus();
            userStatusDtos = userStatusDtos.ToList();
            ViewData["userStatusDtos"] = new SelectList(userStatusDtos, "Id", "StatusName", n.UserStatusId);

            IEnumerable<BranchDto> branchs = await _unitOfWork.Branch.GetAllBranch();
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
                var n = await _unitOfWork.User.UpdateUser(_UserDto);
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
                    await _unitOfWork.Action.CreateAction(IdUser, 2, "Thay đổi dữ liệu Người Dùng {" + _UserDto.FullName + "}");
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
                var listAccount = await _unitOfWork.UserAccount.GetAllUserAccount();
                foreach (var id in ids)
                {
                    var n = await _unitOfWork.User.GetUser(id);
                    dl = await _unitOfWork.User.DeleteUser(n);
                    //Tự động Thêm Hitory action
                    int IdUser = 0;
                    if (Request.Cookies.TryGetValue("IdUser", out string intString))
                    {
                        if (int.TryParse(intString, out int myIntValue))
                        {
                            IdUser = myIntValue;
                        }
                    }
                    await _unitOfWork.Action.CreateAction(IdUser, 3, "Xóa dữ liệu Người Dùng {" + n.FullName + "}");
                    foreach (var item in listAccount)
                    {
                        if (item != null)
                        {
                            if(item.UserId == id)
                            {
                                var dlac = await _unitOfWork.UserAccount.DeleteUserAccount(item);
                                //Tự động Thêm Hitory action
                                await _unitOfWork.Action.CreateAction(IdUser, 3, "Xóa dữ liệu Tài Khoản {" + item.Username + "}");
                                break;
                            }
                        }
                    }
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
            List<UserDto> list = await _unitOfWork.User.GetAllUser() as List<UserDto>;

            string json = JsonConvert.SerializeObject(list);

            // Sử dụng biến json như bạn muốn, ví dụ:
            return Content(json, "application/json");
        }

        [Area("Admin")]
        [HttpPost]
        public async Task<IActionResult> GetUserType(int id)
        {
            // Trong một action method hoặc User
            UserTypeDto n = await _unitOfWork.UserType.GetUserType(id);
            string json = JsonConvert.SerializeObject(n);
            // Sử dụng biến json như bạn muốn, ví dụ:
            return Content(json, "application/json");
        }

        [Area("Admin")]
        [HttpPost]
        public async Task<IActionResult> GetUserStatus(int id)
        {
            // Trong một action method hoặc User
            UserStatusDto n = await _unitOfWork.UserStatus.GetUserStatus(id);
            string json = JsonConvert.SerializeObject(n);
            // Sử dụng biến json như bạn muốn, ví dụ:
            return Content(json, "application/json");
        }

        [Area("Admin")]
        [HttpPost]
        public async Task<IActionResult> GetBranch(int id)
        {
            // Trong một action method hoặc User
            BranchDto n = await _unitOfWork.Branch.GetBranch(id);
            string json = JsonConvert.SerializeObject(n);
            // Sử dụng biến json như bạn muốn, ví dụ:
            return Content(json, "application/json");
        }
    }
}
