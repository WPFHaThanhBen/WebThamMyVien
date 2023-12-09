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

        [Route("GetListProductByType")]
        [HttpPost]
        public async Task<IActionResult> GetListProductByType(string listId)
        {
            List<ProductView> listProductView = new List<ProductView>();

            List<string> listId_string = JsonConvert.DeserializeObject<List<string>>(listId);
            // Trong một action method hoặc Invoice
            foreach (var item  in listId_string)
            {
                List<ProductDto> list = await _unitOfWork.Product.GetAllProductByType(int.Parse(item)) as List<ProductDto>;
                if(list != null)
                {
                    foreach (var product in list)
                    {
                        ProductView productView = new ProductView();
                        productView.Product = product;
                        productView.Images = await _unitOfWork.ProductImage.GetAllProductImageByProduct(product.Id) as List<ProductImageDto>;
                        var promotion = await _unitOfWork.Promotion.GetPromotion((int)product.AppliedPromotionId) as PromotionDto;
                        productView.Promotion = int.Parse(promotion.PromotionValue);
                        listProductView.Add(productView);
                    }
                }

            }

            string json = JsonConvert.SerializeObject(listProductView);

            // Sử dụng biến json như bạn muốn, ví dụ:
            return Content(json, "application/json");
        }

        [Route("Carts")]
        public async Task<IActionResult> Carts()
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

            WebProduct webProduct = new WebProduct();
            List<ProductView> listProductView = new List<ProductView>();
            List<ServiceTypeDto> list = await _unitOfWork.ServiceType.GetAllServiceType() as List<ServiceTypeDto>;
            ViewData["menuDefault"] = "";
            ViewData["menuDefaultServiceTypeDto"] = list;

            // Lấy giá trị int từ cookie (nếu tồn tại)
            int IdAccount = 0;
            if (Request.Cookies.TryGetValue("IdAccount", out string intString))
            {
                if (int.TryParse(intString, out int myIntValue))
                {
                    IdAccount = myIntValue;
                }
            }
            // List Data Send to View
            List<ShoppingCartItemView> listShoppingCartItemView = new List<ShoppingCartItemView>();
            if (IdAccount > 0)
            {
                var shoppingCart = await _unitOfWork.ShoppingCart.GetShoppingCartByAccountId(IdAccount) as ShoppingCartDto;
                if(shoppingCart == null)
                {
                    return View(listShoppingCartItemView);
                }
                var listShoppingCartItem = await _unitOfWork.ShoppingCartItem.GetAllShoppingCartItemByShoppingCartId(shoppingCart.Id) as List<ShoppingCartItemDto>;
                if (listShoppingCartItem == null)
                {
                    return View(listShoppingCartItemView);
                }
                foreach(var item in listShoppingCartItem)
                {
                    // ShoppingCartItemDto - > ShoppingCartItemView 
                    ShoppingCartItemView shoppingCartItemView = new ShoppingCartItemView();
                    shoppingCartItemView.Id = item.Id;
                    shoppingCartItemView.Quantity = item.Quantity;
                    ProductView productView = new ProductView();
                    var product = await _unitOfWork.Product.GetProduct((int)item.ProductId) as ProductDto;
                    productView.Product = product;
                    var promotion = await _unitOfWork.Promotion.GetPromotion((int)product.AppliedPromotionId) as PromotionDto;
                    productView.Promotion = int.Parse(promotion.PromotionValue);
                    productView.Images = await _unitOfWork.ProductImage.GetAllProductImageByProduct(product.Id) as List<ProductImageDto>;
                    shoppingCartItemView.ProductView = productView;
                    double a = (double)productView.Promotion;
                    double b = (double)product.SellingPrice;
                    // Tính tiền sau khuyến mãi
                    double priceItem = b * (1 - a / 100);
                    shoppingCartItemView.Price = (int)priceItem;
                    listShoppingCartItemView.Add(shoppingCartItemView);
                }
            }
            return View(listShoppingCartItemView);
        }

        [Route("UpdatePriceShoppingCartItem")]
        [HttpPost]
        public async Task<IActionResult> UpdatePriceShoppingCartItem(int idProduct, int quantity)
        {
            string json = "";
            try
            {
                var product = await _unitOfWork.Product.GetProduct((int)idProduct) as ProductDto;
                var promotion = await _unitOfWork.Promotion.GetPromotion((int)product.AppliedPromotionId) as PromotionDto;
                int promotionValue = int.Parse(promotion.PromotionValue);
                int priceProduct = (int)product.SellingPrice;
                double a = (double)promotionValue;
                double b = (double)priceProduct;
                // Tính tiền sau khuyến mãi
                double priceItem = b * (1 - a / 100);
                json = JsonConvert.SerializeObject(priceItem * quantity);
            }
            catch
            {
                json = JsonConvert.SerializeObject(false);
            }

            // Sử dụng biến json như bạn muốn, ví dụ:
            return Content(json, "application/json");
        }

        [Route("DeleteShoppingCartItem")]
        [HttpPost]
        public async Task<IActionResult> DeleteShoppingCartItem(int id)
        {
            string json = "";
            try
            {
                var item = await _unitOfWork.ShoppingCartItem.GetShoppingCartItem((int)id) as ShoppingCartItemDto;
                var deleteItem = await _unitOfWork.ShoppingCartItem.DeleteShoppingCartItem(item);
                json = JsonConvert.SerializeObject(deleteItem);
            }
            catch
            {
                json = JsonConvert.SerializeObject(false);
            }

            // Sử dụng biến json như bạn muốn, ví dụ:
            return Content(json, "application/json");
        }

        [Route("CreateShoppingCartItem")]
        [HttpPost]
        public async Task<IActionResult> CreateShoppingCartItem(string id)
        {
            string json;
            try
            {
                ShoppingCartItemDto shoppingCartItemDto = new ShoppingCartItemDto();
                var product = await _unitOfWork.Product.GetProduct(int.Parse(id));
                // Lấy giá trị int từ cookie (nếu tồn tại)
                string idAccountValue = Request.Cookies["IdAccount"];
                int IdAccount = 0;
                // Kiểm tra xem cookie có tồn tại không
                if (idAccountValue != null)
                {
                    IdAccount = Convert.ToInt32(idAccountValue);
                }
                if (IdAccount > 0)
                {
                    var shoppingCart = await _unitOfWork.ShoppingCart.GetShoppingCartByAccountId(IdAccount) as ShoppingCartDto;
                    if (shoppingCart == null)
                    {
                        json = JsonConvert.SerializeObject(false);
                        return Content(json, "application/json");
                    }

                    var listShoppingCartItem = await _unitOfWork.ShoppingCartItem.GetAllShoppingCartItemByShoppingCartId(shoppingCart.Id) as List<ShoppingCartItemDto>;
                    int trungProduct = 0;
                    foreach (var item in listShoppingCartItem)
                    {
                        if(item.ProductId == product.Id)
                        {
                            trungProduct += 1;
                        }
                    }
                    if(trungProduct == 0)
                    {
                        shoppingCartItemDto.Id = 0;
                        shoppingCartItemDto.ShoppingCartId = shoppingCart.Id;
                        shoppingCartItemDto.ProductId = product.Id;
                        shoppingCartItemDto.Price = product.SellingPrice;
                        shoppingCartItemDto.Quantity = 1;
                        shoppingCartItemDto.ServiceId = null;

                        var CreateshoppingCart = await _unitOfWork.ShoppingCartItem.CreateShoppingCartItem(shoppingCartItemDto);
                        if (CreateshoppingCart)
                        {
                            json = JsonConvert.SerializeObject("1");
                        }
                        else
                        {
                            json = JsonConvert.SerializeObject("2");
                        }
                        return Content(json, "application/json");
                    }
                    else
                    {
                        json = JsonConvert.SerializeObject("3");
                        return Content(json, "application/json");
                    }

                }
            }
            catch
            {
                json = JsonConvert.SerializeObject("2");
            }
            json = JsonConvert.SerializeObject("2");
            // Sử dụng biến json như bạn muốn, ví dụ:
            return Content(json, "application/json");
        }

        [Route("GetProductIdByShoppingCartId")]
        [HttpPost]
        public async Task<IActionResult> GetProductIdByShoppingCartId(int id)
        {
            string json = "";
            try
            {
                var item = await _unitOfWork.ShoppingCartItem.GetShoppingCartItem((int)id) as ShoppingCartItemDto;
                json = JsonConvert.SerializeObject(item.ProductId);
            }
            catch
            {
                json = JsonConvert.SerializeObject(false);
            }

            // Sử dụng biến json như bạn muốn, ví dụ:
            return Content(json, "application/json");
        }
        //Bổ xung thêm các Action Add Item Cart, Delete, Update, Thêm Item ..... 
        //Nâng cấp thêm tính năng thanh toán online và 3 loại ví như VNPlay, MoMo, ...
    }
}