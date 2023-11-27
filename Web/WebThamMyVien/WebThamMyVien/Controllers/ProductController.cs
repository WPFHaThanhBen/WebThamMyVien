using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Diagnostics;
using WebThamMyVien.Interfaces;
using WebThamMyVien.Models;

namespace WebThamMyVien.Controllers
{
    [Route("Product")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            WebProduct webProduct = new WebProduct();
            List<ProductView> listProductView = new List<ProductView>();
            List<ServiceTypeDto> list = await _unitOfWork.ServiceType.GetAllServiceType() as List<ServiceTypeDto>;
            ViewData["menuDefault"] = "MyPham";
            ViewData["menuDefaultServiceTypeDto"] = list;

            var ListProduct = (List<ProductDto>)await _unitOfWork.Product.GetAllProduct() as List<ProductDto>;
            foreach (var item in ListProduct)
            {
                ProductView productView = new ProductView();
                productView.Product = item;
                var promotion = await _unitOfWork.Promotion.GetPromotion((int)item.AppliedPromotionId);
                productView.Promotion = int.Parse(promotion.PromotionValue);
                productView.Images = (List<ProductImageDto>)await _unitOfWork.ProductImage.GetAllProductImageByProduct(item.Id);
                listProductView.Add(productView);
            }
            var listProductType = await _unitOfWork.ProductType.GetAllProductType() as List<ProductTypeDto>;
            List<ProductTypeView> listProductTypeView = new List<ProductTypeView>();
            foreach (var item in listProductType)
            {
                ProductTypeView productTypeView = new ProductTypeView();
                productTypeView.ProductType = item;
                var mnmnm = await _unitOfWork.Product.GetAllProductByType(item.Id);
                if(mnmnm!= null)
                {
                    productTypeView.Quantity = mnmnm.Count();
                }
                else
                {
                    productTypeView.Quantity = 0;
                }
                listProductTypeView.Add(productTypeView);
            }
            webProduct.ListProductView = listProductView;
            webProduct.ListProductType = listProductTypeView;
            // Gán giá trị vào ViewBag hoặc ViewModel
            return View(webProduct);
        }

        [Route("Detail")]
        public async Task<IActionResult> Detail(int id)
        {
            ProductView productView = new ProductView();
            List<ServiceTypeDto> list = await _unitOfWork.ServiceType.GetAllServiceType() as List<ServiceTypeDto>;
            ViewData["menuDefault"] = "MyPham";
            ViewData["menuDefaultServiceTypeDto"] = list;

            var product = await _unitOfWork.Product.GetProduct(id) as ProductDto;
            productView.Product = product;
            var promotion = await _unitOfWork.Promotion.GetPromotion((int)product.AppliedPromotionId);
            productView.Promotion = int.Parse(promotion.PromotionValue);

            var listImage = await _unitOfWork.ProductImage.GetAllProductImageByProduct((int)product.Id) as List<ProductImageDto>;

            productView.Images = listImage;
            // Gán giá trị vào ViewBag hoặc ViewModel
            return View(productView);
        }
    }
}