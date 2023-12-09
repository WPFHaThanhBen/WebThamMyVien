using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Security.Principal;
using WebThamMyVien.Interfaces;
using WebThamMyVien.Models;

namespace WebThamMyVien.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            // Đọc giá trị của cookie "IdAccount" từ request
            string idAccountValue = Request.Cookies["IdAccount"];

            // Kiểm tra xem cookie có tồn tại không
            if (idAccountValue != null)
            {
                if(idAccountValue == "1")
                {
                    Response.Cookies.Append("IdAccount", "", new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(30) // Thời hạn 30 ngày
                    });
                    ViewData["IdAccount"] = null;
                }
                else
                {
                    int idAccount = Convert.ToInt32(idAccountValue);
                    ViewData["IdAccount"] = idAccount;
                }
            }
            else
            {
                ViewData["IdAccount"] = null;
            }

            WebHome webHome = new WebHome();
            ViewData["menuDefault"] = "TrangChu";

            // Get List Item Menu DichVu
            List<ServiceTypeDto> list = await _unitOfWork.ServiceType.GetAllServiceType() as List<ServiceTypeDto>;
            ViewData["menuDefaultServiceTypeDto"] = list;

            // Get #1 Post start
            webHome.OnePost = await _unitOfWork.Post.GetPost(1);
            webHome.ListPost = (List<PostDto>)await _unitOfWork.Post.GetPostByPostType(1);
            webHome.ListPost1 = (List<PostDto>)await _unitOfWork.Post.GetPostByPostType(2);
            webHome.ListPost2 = (List<PostDto>)await _unitOfWork.Post.GetPostByPostType(3);
            var ListProduct = (List<ProductDto>)await _unitOfWork.Product.GetAllProductByType(1);
            foreach (var item in ListProduct)
            {
                ProductView productView = new ProductView();
                productView.Product = item;
                var promotion = await _unitOfWork.Promotion.GetPromotion((int)item.AppliedPromotionId);
                productView.Promotion = int.Parse(promotion.PromotionValue);  
                productView.Images = (List<ProductImageDto>)await _unitOfWork.ProductImage.GetAllProductImageByProduct(item.Id);
                webHome.ListProductView.Add(productView);
            }
            if (webHome.ListPost.Count > 0)
            {
                webHome.ListPost.RemoveAt(0); // Xóa phần tử đầu tiên
            }
            return View(webHome);
        }
    }
}