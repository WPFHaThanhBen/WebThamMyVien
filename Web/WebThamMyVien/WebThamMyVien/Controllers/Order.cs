using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using WebThamMyVien.Interfaces;
using WebThamMyVien.Models;

namespace WebThamMyVien.Controllers
{
    [Route("Order")]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            // Đọc giá trị của cookie "IdAccount" từ request
            string idAccountValue = Request.Cookies["IdAccount"];

            // Kiểm tra xem cookie có tồn tại không
            if (idAccountValue != null)
            {
                int idAccount = Convert.ToInt32(idAccountValue);
                ViewData["IdAccount"] = idAccount;
            }
            else
            {
                ViewData["IdAccount"] = null;
            }

            return View();
        }

        [Route("Create")]
        public async Task<IActionResult> Create(string id)
        {
            // Đọc giá trị của cookie "IdAccount" từ request
            string idAccountValue = Request.Cookies["IdAccount"];

            // Kiểm tra xem cookie có tồn tại không
            if (idAccountValue != null)
            {
                int idAccount = Convert.ToInt32(idAccountValue);
                ViewData["IdAccount"] = idAccount;
            }
            else
            {
                ViewData["IdAccount"] = null;
            }

            List<ServiceTypeDto> list = await _unitOfWork.ServiceType.GetAllServiceType() as List<ServiceTypeDto>;
            ViewData["menuDefault"] = "MyPham";
            ViewData["menuDefaultServiceTypeDto"] = list;
            // Gán giá trị vào ViewBag hoặc ViewModel
            return View();
        }

        [Route("GetListOrderByType")]
        [HttpPost]
        public async Task<IActionResult> GetListOrderByType(string listId)
        {

            List<string> listId_string = JsonConvert.DeserializeObject<List<string>>(listId);
            // Trong một action method hoặc Invoice
            foreach (var item in listId_string)
            {


            }

            string json = JsonConvert.SerializeObject(true);

            // Sử dụng biến json như bạn muốn, ví dụ:
            return Content(json, "application/json");
        }

    }

}