using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol.Plugins;
using WebThamMyVien.Interfaces;
using WebThamMyVien.Models;

namespace WebThamMyVien.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public LoginController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index(LoginDto? value)
        {
            return View();
        }
        public IActionResult Register(RegisterDto? value)
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UserLogin(string login)
        {
            string json = JsonConvert.SerializeObject(4);
            LoginDto loginDto = JsonConvert.DeserializeObject<LoginDto>(login);

            if (loginDto != null)
            {
               bool statusLogin = await _unitOfWork.UserAccount.Login(loginDto.Username.ToLower(), loginDto.Password.ToLower());
                if(statusLogin)
                {
                    if(loginDto.Username == "Admin")
                    {
                        // Là Admin
                        json = JsonConvert.SerializeObject(2);
                        // Đặt giá trị int vào cookie
                        Response.Cookies.Append("IdAccount", "1", new CookieOptions
                        {
                            Expires = DateTime.Now.AddDays(30) // Thời hạn 30 ngày
                        });
                    }
                    else
                    {
                        UserAccountDto statusLogin1 = await _unitOfWork.UserAccount.GetUserAccountByUserName(loginDto.Username);
                        if(statusLogin1.AccountTypeId == 4)
                        {
                            // Là Khách hàng
                            json = JsonConvert.SerializeObject(1);
                            // Đặt giá trị int vào cookie
                            Response.Cookies.Append("IdAccount", statusLogin1.Id.ToString(), new CookieOptions
                            {
                                Expires = DateTime.Now.AddDays(30) // Thời hạn 30 ngày
                            });
                        }
                        else
                        {
                            // Là Nhân viên
                            json = JsonConvert.SerializeObject(3);
                            // Đặt giá trị int vào cookie
                            Response.Cookies.Append("IdAccount", statusLogin1.Id.ToString(), new CookieOptions
                            {
                                Expires = DateTime.Now.AddDays(30) // Thời hạn 30 ngày
                            });
                        }
                    }
                }
                else
                {
                    // Lỗi login
                    json = JsonConvert.SerializeObject(4);
                    // Đặt giá trị int vào cookie
                    Response.Cookies.Append("IdAccount", "", new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(30) // Thời hạn 30 ngày
                    });
                }
            }
            // Trả về kết quả các số tương ứng với các trạng thái
            //1 login thành công và là CUstomer
            //2 Login thành công và là Admin
            //3 Login thành công và là Nhanvien
            //4 Login thất bại
            return Content(json, "application/json");
        }

        // Action đăng ký tài khoản
        [HttpPost]
        public async Task<IActionResult> UserRegister(string register)
        {
            // Khởi tạo biến json return cho view
            string json = JsonConvert.SerializeObject(2);
            // Khởi tạo object Register từ json Ajax gửi qua
            RegisterDto registerDto = JsonConvert.DeserializeObject<RegisterDto>(register);
            if (registerDto != null)
            {
                // Khởi tạo object Tài khoản
                UserAccountDto account = new UserAccountDto();
                account.Id = 0;
                account.Username = registerDto.UserName.ToLower();
                account.Password = registerDto.PassWord.ToLower();
                account.UserStatusId = 1;
                account.Email = registerDto.Email;
                account.AccountTypeId = 4;
                bool statusCreateAccount = await _unitOfWork.UserAccount.CreateUserAccount(account);
                if (statusCreateAccount)
                {
                    // Create object Khách hàng
                    CustomerDto customer = new CustomerDto();
                    customer.Id = 0;
                    customer.Address = registerDto.Address;
                    customer.FullName = registerDto.FullName;
                    customer.PhoneNumber = registerDto.PhoneNumber;
                    customer.Email = registerDto.Email;
                    bool statusCreateCustomer = await _unitOfWork.Customer.CreateCustomer(customer);
                    if (statusCreateCustomer)
                    {
                        // Create object Giỏ hàng
                        ShoppingCartDto shoppingCartDto = new ShoppingCartDto();
                        shoppingCartDto.Id = 0;
                        UserAccountDto ac = await _unitOfWork.UserAccount.GetUserAccountByUserName(registerDto.UserName);
                        shoppingCartDto.UserAccountId = ac.Id;
                        bool statusCreateShoppingCart = await _unitOfWork.ShoppingCart.CreateShoppingCart(shoppingCartDto);
                        if(statusCreateShoppingCart)
                        {
                            json = JsonConvert.SerializeObject(1);
                            // Update object Tài khoản
                            var customerFinal = await _unitOfWork.Customer.GetCustomerFinal();
                            ac.CustomerId = customerFinal.Id;
                            await _unitOfWork.UserAccount.UpdateUserAccount(ac);
                        }
                        else
                        {
                            // If Create Cart = false => Delete Customer
                            CustomerDto ctm = await _unitOfWork.Customer.GetCustomerFinal();
                            _unitOfWork.Customer.DeleteCustomer(ctm);
                            // If Create Cart = false => Delete Account
                            UserAccountDto uac = await _unitOfWork.UserAccount.GetUserAccountByUserName(registerDto.UserName);
                            _unitOfWork.UserAccount.DeleteUserAccount(uac);
                            json = JsonConvert.SerializeObject(2);
                        }
                    }
                    else
                    {
                        // If Create Customer = false => Delete Account
                        UserAccountDto uac = await _unitOfWork.UserAccount.GetUserAccountByUserName(registerDto.UserName);
                        _unitOfWork.UserAccount.DeleteUserAccount(uac);
                        json = JsonConvert.SerializeObject(2);
                    }
                }
                else
                {
                    // Error Create Account
                    json = JsonConvert.SerializeObject(2);
                }
            }
            // Trả về kết quả các số tương ứng với các trạng thái
            //1 Create account true
            //2 Create Accout false
            return Content(json, "application/json");
        }

        [HttpPost]
        public async Task<IActionResult> CheckAccount(string sdt,string username)
        {
            string json = JsonConvert.SerializeObject(1);
            string phoneCustomer = JsonConvert.DeserializeObject<string>(sdt);
            string usernameAccount = JsonConvert.DeserializeObject<string>(username);
            // Get Customer by phone
            var checkCustomer = await _unitOfWork.Customer.GetCustomerBySDT(phoneCustomer);
            // Get Account by username
            var checkAccount = await _unitOfWork.UserAccount.GetUserAccountByUserName(usernameAccount);
            if (checkAccount != null)
            {
                // Phone đã đước sử dụng
                json = JsonConvert.SerializeObject(3);
                return Content(json, "application/json");
            }
            if (checkCustomer != null)
            {
                // Username đã được sử dụng
                json = JsonConvert.SerializeObject(2);
                return Content(json, "application/json");
            }
            // Có thể tạo Tài khoản
            json = JsonConvert.SerializeObject(1);
            return Content(json, "application/json");
        }


    }
}
