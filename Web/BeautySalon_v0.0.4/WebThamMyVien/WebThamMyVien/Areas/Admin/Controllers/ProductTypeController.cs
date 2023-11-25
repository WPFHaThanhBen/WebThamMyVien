using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Common;
using WebThamMyVien.Interfaces;
using WebThamMyVien.Models;
using X.PagedList;

namespace WebThamMyVien.Areas.Admin.Controllers
{
    public class ProductTypeController : Controller
    {
        private readonly IProductTypeRepository _productTypeRepository;

        public ProductTypeController(IProductTypeRepository productTypeRepository)
        {
            _productTypeRepository = productTypeRepository;
        }

        [Area("Admin")]
        public async Task<IActionResult> Index()
        {
            TempData["menu"] = "ProductType";
            return View();
        }

        [Area("Admin")]
        public async Task<IActionResult> Create(ProductTypeDto? value)
        {
            // Khởi tạo Menu (bắt buộc)
            TempData["menu"] = "ProductType";
            // ProductTypeDto hợp lệ
            if (ModelState.IsValid)
            {
                // Add data in DB
                var n = await _productTypeRepository.CreateProductType(value);
                // Add status is success
                if (n)
                {
                    TempData["success"] = "Thêm mới thông tin thành công";
                    return RedirectToAction("Index");
                }
                // Add status is error
                else
                {
                    TempData["error"] = "Thêm mới thông tin thất bại";
                }
            }
            // ProductTypeDto không hợp lệ
            else
            {
                if (value.ProductTypeName == "" || value.ProductTypeName == null)
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
            TempData["menu"] = "ProductType";
            TempData["EditProductType"] = id;
            // Get ProductType in DB
            var n = await _productTypeRepository.GetProductType((int)id);
            return View(n);
        }

        [Area("Admin")]
        public async Task<IActionResult> Edit(ProductTypeDto? value)
        {
            // ProductTypeDto hợp lệ
            if (ModelState.IsValid)
            {
                // Update data in Db
                var n = await _productTypeRepository.UpdateProductType(value);
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
            // ProductTypeDto không hợp lệ
            else
            {
                TempData["warning"] = "Thông tin không hợp lệ";
            }

            return RedirectToAction("Detail", new { id = TempData["EditProductType"] });
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
                    var n = await _productTypeRepository.GetProductType(id);
                    dl = await _productTypeRepository.DeleteProductType(n);

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
        public  async Task<IActionResult> GetALL()
        {
            // Trong một action method hoặc service
            List<ProductTypeDto> list = await _productTypeRepository.GetAllProductType() as List<ProductTypeDto>;

            string json = JsonConvert.SerializeObject(list);

            // Sử dụng biến json như bạn muốn, ví dụ:
            return Content(json, "application/json");
        }
    }
}
