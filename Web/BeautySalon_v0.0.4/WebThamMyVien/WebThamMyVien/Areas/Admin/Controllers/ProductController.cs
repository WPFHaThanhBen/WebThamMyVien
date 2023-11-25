using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using WebThamMyVien.Interfaces;
using WebThamMyVien.Models;

namespace WebThamMyVien.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductTypeRepository _productTypeRepository;
        private readonly IPromotionRepository _promotionRepository;

        public ProductController(IProductRepository ProductRepository, IProductTypeRepository ProductTypeRepository, IPromotionRepository promotionRepository)
        {
            _productRepository = ProductRepository;
            _productTypeRepository = ProductTypeRepository;
            _promotionRepository = promotionRepository;
        }

        [Area("Admin")]
        public async Task<IActionResult> Index()
        {
            TempData["menu"] = "Product";
            return View();
        }

        [Area("Admin")]
        public async Task<IActionResult> Create(ProductDto? _productDto)
        {
            // Khởi tạo Menu (bắt buộc)
            TempData["menu"] = "Product";

            IEnumerable<ProductTypeDto> ProductTypes = await _productTypeRepository.GetAllProductType();
            ProductTypes = ProductTypes.ToList();
            ViewData["ProductType"] = new SelectList(ProductTypes, "Id", "ProductTypeName");

            // lấy value cho Cbb PromotionDto
            IEnumerable<PromotionDto> promotions = await _promotionRepository.GetAllPromotion();
            promotions = promotions.ToList();
            ViewData["promotion"] = new SelectList(promotions, "Id", "PromotionName");

            // ProductDto hợp lệ
            if (ModelState.IsValid)
            {
                // Add data in DB
                var n = await _productRepository.CreateProduct(_productDto);
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
            // ProductDto không hợp lệ
            else
            {
                if (_productDto.ProductName == "" || _productDto.ProductName == null)
                {
                    return View(_productDto);
                }
                TempData["warning"] = "Thông tin không hợp lệ";
            }
            return View(_productDto);
        }

        [Area("Admin")]
        public async Task<IActionResult> Detail(int? id = 1)
        {
            var n = await _productRepository.GetProduct((int)id);
            IEnumerable<ProductTypeDto> ProductTypes = await _productTypeRepository.GetAllProductType();
            ProductTypes = ProductTypes.ToList();
            ViewData["ProductType"] = new SelectList(ProductTypes, "Id", "ProductTypeName", n.ProductTypeId);

            // lấy value cho Cbb PromotionDto
            IEnumerable<PromotionDto> promotions = await _promotionRepository.GetAllPromotion();
            promotions = promotions.ToList();
            ViewData["promotion"] = new SelectList(promotions, "Id", "PromotionName", n.AppliedPromotionId);
            // Khởi tạo Menu (bắt buộc)
            TempData["menu"] = "Product";
            TempData["EditID"] = id;
            // Get Product in DB

            return View(n);
        }

        [Area("Admin")]
        public async Task<IActionResult> Edit(ProductDto? _productDto)
        {
            // _productDto hợp lệ
            if (ModelState.IsValid)
            {
                // Update data in Db
                var n = await _productRepository.UpdateProduct(_productDto);
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
            // _productDto không hợp lệ
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
                    var n = await _productRepository.GetProduct(id);
                    dl = await _productRepository.DeleteProduct(n);
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
            // Trong một action method hoặc Product
            List<ProductDto> list = await _productRepository.GetAllProduct() as List<ProductDto>;

            string json = JsonConvert.SerializeObject(list);

            // Sử dụng biến json như bạn muốn, ví dụ:
            return Content(json, "application/json");
        }

        [Area("Admin")]
        [HttpPost]
        public async Task<IActionResult> GetProductType(int id)
        {
            // Trong một action method hoặc Product
            ProductTypeDto n = await _productTypeRepository.GetProductType(id);
            string json = JsonConvert.SerializeObject(n);
            // Sử dụng biến json như bạn muốn, ví dụ:
            return Content(json, "application/json");
        }

        [Area("Admin")]
        [HttpPost]
        public async Task<IActionResult> GetPromotion(int id)
        {
            // Trong một action method hoặc Product
            PromotionDto n = await _promotionRepository.GetPromotion(id);
            string json = JsonConvert.SerializeObject(n);
            // Sử dụng biến json như bạn muốn, ví dụ:
            return Content(json, "application/json");
        }
    }
}
