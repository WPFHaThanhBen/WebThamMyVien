using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using WebThamMyVien.Helper;
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

        [Route("Detail")]
        public async Task<IActionResult> Detail(string id, string? tool)
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
            OrderView orderView = new OrderView();
            List<OrderDetailView> listOrderDetailView = new List<OrderDetailView>();
            var oderDetail = await _unitOfWork.OrderDetail.GetAllOrderDetailByOrderId(Convert.ToInt32(id));
            // Thêm các Item order detail
            foreach(var item in oderDetail)
            {
                OrderDetailView orderDetailView = new OrderDetailView();
                orderDetailView.Id = item.Id;
                orderDetailView.SeqNumber = item.SeqNumber;
                orderDetailView.OrderId = item.OrderId;
                orderDetailView.ProductId = item.ProductId;
                orderDetailView.Quantity = item.Quantity;
                orderDetailView.TotalPrice = item.TotalPrice;

                var product = await _unitOfWork.Product.GetProduct(Convert.ToInt32(item.ProductId));
                orderDetailView.productName = product.ProductName;
                orderDetailView.Images = (List<ProductImageDto>)await _unitOfWork.ProductImage.GetAllProductImageByProduct(Convert.ToInt32(item.ProductId));
                listOrderDetailView.Add(orderDetailView);
            }
            orderView.OrderDetails = listOrderDetailView;
            var order = await _unitOfWork.Order.GetOrder(Convert.ToInt32(id));
            if (tool == "update1")
            {
                order.OrderStatusId = 2;
                orderView.OrderStatusId = 2;
                order.TotalAmount = 0;
                orderView.TotalAmount = 0;
                await _unitOfWork.Order.UpdateOrder(order);

            }
            orderView.Id = order.Id;
            orderView.OrderStatusId = order.OrderStatusId;
            var orderStatus = await _unitOfWork.OrderStatus.GetOrderStatus(Convert.ToInt32(order.OrderStatusId));
            orderView.orderStatusName = orderStatus.StatusName;
            orderView.OrderDate = order.OrderDate;
            if (orderView.TotalAmount > 0)
            {
                orderView.dathanhtoan = false;
            }
            else
            {
                orderView.dathanhtoan = false;
            }
            orderView.Other = order.Other;
            orderView.RecipientPhoneNumber = order.RecipientPhoneNumber;
            orderView.CustomerId = order.CustomerId;
            orderView.TotalAmount = order.TotalAmount;
            orderView.DeliveryAddress = order.DeliveryAddress;




            return View(orderView);
        }

        [Route("CreateOrder")]
        [HttpPost]
        public async Task<IActionResult> CreateOrder(string listProduct, string ghichu)
        {
            string json = JsonConvert.SerializeObject(0);
            // listProduct => Object(IdProduct,Quantity) được chọn để tạo đơn hàng
            // idShoppingCart => Id giỏ hàng, dùng để xóa các itemDetail của nó khi  tạo đơn hàng thành công
            // ghichu => ghi chú của đơn hàng
            //==========================================================
            // Create Order
            // Create OrderDetail
            // Delete Shopping CartItem
            // Return Order.Id
            //==========================================================

            List<ObjectItem> listObjectItem = JsonConvert.DeserializeObject<List<ObjectItem>>(listProduct);
            if (listObjectItem.Count != 0)
            {

                // Cần có Customer.Id,  SDT và địa chỉ để định danh đơn hàng
                // Để có SDT ta cần lấy Customer đang login
                // Để có Customer đang login ta cần lấy Id của Account đang login
                
                // Đọc giá trị của cookie "IdAccount" từ request
                string idAccountValue = Request.Cookies["IdAccount"];
                if (idAccountValue != null)
                {
                    int orderTotalPrice = 0;
                    // Get Account ID by Account.Id
                    var account = await _unitOfWork.UserAccount.GetUserAccount(Convert.ToInt32(idAccountValue));
                    var customer = await _unitOfWork.Customer.GetCustomer(Convert.ToInt32(account.CustomerId));

                    OrderDto order = new OrderDto();
                    order.Id = 0;
                    order.OrderStatusId = 1;
                    order.OrderDate = Support.GetCurrentDateTime();
                    order.BranchId = null;
                    order.CustomerId = customer.Id;
                    order.TotalAmount = 0;
                    order.DeliveryAddress = customer.Address;
                    order.RecipientPhoneNumber = customer.PhoneNumber;
                    order.Other = ghichu;
                    await _unitOfWork.Order.CreateOrder(order);
                    // Order đã Create
                    var orderFinal = await _unitOfWork.Order.GetOrderFinal();
                    int numberIndex = 1;
                    // Tạo vòng lặp create OrderDetail
                    foreach (var item in listObjectItem)
                    {
                        OrderDetailDto detail = new OrderDetailDto();
                        detail.Id = 0;
                        detail.SeqNumber = numberIndex; numberIndex += 1;
                        detail.OrderId = orderFinal.Id;
                        var shoppingCartItem = await _unitOfWork.ShoppingCartItem.GetShoppingCartItem(item.id);
                        var product = await _unitOfWork.Product.GetProduct((int)shoppingCartItem.ProductId);
                        detail.ProductId = product.Id;
                        detail.Quantity = item.quantity;
                        var promotion = await _unitOfWork.Promotion.GetPromotion((int)product.AppliedPromotionId);
                        detail.PromotionId = null;
                        // Tổng tiền chưa giảm giá
                        double totalPrice = Convert.ToDouble(product.SellingPrice) * Convert.ToDouble(item.quantity);
                        // Tổng số tiền giảm giá
                        double totalPriceTru = ((Convert.ToDouble(product.SellingPrice) / 100) * Convert.ToDouble(promotion.PromotionValue))* Convert.ToDouble(item.quantity);
                        detail.TotalPrice = Convert.ToInt32(totalPrice - totalPriceTru);
                        // Lưu lại tổng số tiền của item trên
                        orderTotalPrice += (int)detail.TotalPrice;
                        await _unitOfWork.OrderDetail.CreateOrderDetail(detail);
                    }
                    var shoppingCart = await _unitOfWork.ShoppingCart.GetShoppingCartByAccountId(Convert.ToInt32(idAccountValue));
                    var listShoppingCartItem = await _unitOfWork.ShoppingCartItem.GetAllShoppingCartItemByShoppingCartId(shoppingCart.Id);
                    // Tạo vòng lặp xóa bỏ các ShoppingCartIem đã tạo đơn hàng
                    foreach (var itemObject in listObjectItem)
                    {
                        foreach (var itemShoppingCart in listShoppingCartItem)
                        {
                            var shoppingCartItem = await _unitOfWork.ShoppingCartItem.GetShoppingCartItem(itemObject.id);
                            if(shoppingCartItem != null)
                            {
                                if (shoppingCartItem.ProductId == itemShoppingCart.ProductId)
                                {
                                    await _unitOfWork.ShoppingCartItem.DeleteShoppingCartItem(itemShoppingCart);
                                    break;
                                }
                            }
                        }

                    }
                    orderFinal.TotalAmount = orderTotalPrice;
                    // Update lại giá của order đã tạo trước đó
                    await _unitOfWork.Order.UpdateOrder(orderFinal);
                    json = JsonConvert.SerializeObject(orderFinal.Id);
                }
                else
                {
                    //Return Lỗi
                    json = JsonConvert.SerializeObject(0);
                }
            }
            return Content(json, "application/json");
        }

        [Route("Payment")]
        public ActionResult Payment(string? tongtien, string? idOrder)
        {
            //request params need to request to MoMo system
            string endpoint = "https://test-payment.momo.vn/gw_payment/transactionProcessor";
            string partnerCode = "MOMOOJOI20210710";
            string accessKey = "iPXneGmrJH0G8FOP";
            string serectkey = "sFcbSGRSJjwGxwhhcEktCHWYUuTuPNDB";
            string orderInfo = "test";
            string returnUrl = "https://localhost:7299/Order/Detail?id=" + idOrder + "&tool=update1";
            string notifyurl = "https://4c8d-2001-ee0-5045-50-58c1-b2ec-3123-740d.ap.ngrok.io/Home/SavePayment"; //lưu ý: notifyurl không được sử dụng localhost, có thể sử dụng ngrok để public localhost trong quá trình test

            string amount = tongtien;
            string orderid = DateTime.Now.Ticks.ToString(); //mã đơn hàng
            string requestId = DateTime.Now.Ticks.ToString();
            string extraData = "";

            //Before sign HMAC SHA256 signature
            string rawHash = "partnerCode=" +
                partnerCode + "&accessKey=" +
                accessKey + "&requestId=" +
                requestId + "&amount=" +
                amount + "&orderId=" +
                orderid + "&orderInfo=" +
                orderInfo + "&returnUrl=" +
                returnUrl + "&notifyUrl=" +
                notifyurl + "&extraData=" +
                extraData;

            MoMoSecurity crypto = new MoMoSecurity();
            //sign signature SHA256
            string signature = crypto.signSHA256(rawHash, serectkey);

            //build body json request
            JObject message = new JObject
            {
                { "partnerCode", partnerCode },
                { "accessKey", accessKey },
                { "requestId", requestId },
                { "amount", amount },
                { "orderId", orderid },
                { "orderInfo", orderInfo },
                { "returnUrl", returnUrl },
                { "notifyUrl", notifyurl },
                { "extraData", extraData },
                { "requestType", "captureMoMoWallet" },
                { "signature", signature }
            };
            string responseFromMomo = PaymentRequest.sendPaymentRequest(endpoint, message.ToString());

            JObject jmessage = JObject.Parse(responseFromMomo);
            return Redirect(jmessage.GetValue("payUrl").ToString());
        }
        public ActionResult ConfirmPaymentClient(Result result)
        {
            //lấy kết quả Momo trả về và hiển thị thông báo cho người dùng (có thể lấy dữ liệu ở đây cập nhật xuống db)
            string rMessage = result.message;
            string rOrderId = result.orderId;
            string rErrorCode = result.errorCode; // = 0: thanh toán thành công
            return View();
        }
        [HttpPost]
        public async Task SavePaymentAsync(string id)
        {
            var order = await _unitOfWork.Order.GetOrder(Convert.ToInt32(id));
            order.TotalAmount = 0;
            order.OrderStatusId = 2;
            await _unitOfWork.Order.UpdateOrder(order);
        }
    }
    public class ObjectItem
    {
        // Thuộc tính (properties) của lớp
        public int id { get; set; }
        public int quantity { get; set; }
    }

}