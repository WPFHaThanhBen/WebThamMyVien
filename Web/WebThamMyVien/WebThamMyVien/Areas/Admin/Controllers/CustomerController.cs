﻿using Microsoft.AspNetCore.Mvc;
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

        [Area("Admin")]
        [HttpPost]
        public async Task<IActionResult> AddImage(int id,string listImage)
        {

            bool n = false;
            if (listImage != null)
            {
                if (listImage.Count() == 1)
                {
                    CustomerImageDto ImagesProduct = JsonConvert.DeserializeObject<CustomerImageDto>(listImage);
                    ImagesProduct.CustomerId = (int)id;
                    n = await _unitOfWork.CustomerImage.CreateCustomerImage(ImagesProduct);
                }
                else
                {
                    List<CustomerImageDto> ImagesProduct = JsonConvert.DeserializeObject<List<CustomerImageDto>>(listImage);
                    foreach (var item in ImagesProduct)
                    {
                        if (item != null)
                        {
                            item.CustomerId = (int)id;
                            n = await _unitOfWork.CustomerImage.CreateCustomerImage(item);
                        }
                    }
                }
            }

            string json = JsonConvert.SerializeObject(n);
            // Sử dụng biến json như bạn muốn, ví d ụ:
            return Content(json, "application/json");
        }

        [Area("Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateImage(int id, string objectImage)
        {
            try
            {
                bool n = false;
                CustomerImageDto ImagesProduct = JsonConvert.DeserializeObject<CustomerImageDto>(objectImage);

                if (ImagesProduct != null)
                {
                    ImagesProduct.CustomerId = (int)id;
                    n = await _unitOfWork.CustomerImage.CreateCustomerImage(ImagesProduct);
                }
                string json = JsonConvert.SerializeObject(n);
                // Sử dụng biến json như bạn muốn, ví d ụ:
                return Content(json, "application/json");
            }
            catch
            {
                string json = JsonConvert.SerializeObject(false);
                // Sử dụng biến json như bạn muốn, ví d ụ:
                return Content(json, "application/json");
            }
        }

        [Area("Admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteImage(int id)
        {
            var delete = await _unitOfWork.CustomerImage.GetCustomerImage(id);
            bool n = false;
            if (delete != null)
            {
                n = await _unitOfWork.CustomerImage.DeleteCustomerImage(delete);
            }
            string json = JsonConvert.SerializeObject(n);
            // Sử dụng biến json như bạn muốn, ví d ụ:
            return Content(json, "application/json");
        }

        [Area("Admin")]
        [HttpPost]
        public async Task<IActionResult> LoadImage(int id)
        {
            var load = await _unitOfWork.CustomerImage.GetAllCustomerImageByCustomer(id);
            string json = JsonConvert.SerializeObject(load);
            // Sử dụng biến json như bạn muốn, ví d ụ:
            return Content(json, "application/json");
        }

        [Area("Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateCustomer(string customer, string images)
        {
            string json;
            // Create Customer
            if (customer != null)
            {
                try
                {
                    CustomerDto customerCreate = JsonConvert.DeserializeObject<CustomerDto>(customer);
                    bool statusCreateCustomer = await _unitOfWork.Customer.CreateCustomer(customerCreate);
                }
                catch
                {
                     json = JsonConvert.SerializeObject(false);
                    // Sử dụng biến json như bạn muốn, ví d ụ:
                    return Content(json, "application/json");
                }

            }
            else
            {
                json = JsonConvert.SerializeObject(false);
                // Sử dụng biến json như bạn muốn, ví d ụ:
                return Content(json, "application/json");
            }
            // Create CustomerImages
            if (images != null)
            {
                try
                {
                    List<CustomerImageDto> customerImageCreate = JsonConvert.DeserializeObject<List<CustomerImageDto>>(images);
                    CustomerDto customerFinal = await _unitOfWork.Customer.GetCustomerFinal();
                    foreach (var item in customerImageCreate)
                    {
                        item.CustomerId = customerFinal.Id;
                        bool statusCreateCustomerImage = await _unitOfWork.CustomerImage.CreateCustomerImage(item);
                    }
                }
                catch
                {
                     json = JsonConvert.SerializeObject(false);
                    // Sử dụng biến json như bạn muốn, ví d ụ:
                    return Content(json, "application/json");
                }

            }
            json = JsonConvert.SerializeObject(true);
            // Sử dụng biến json như bạn muốn, ví d ụ:
            return Content(json, "application/json");
        }
    }
}
