using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;
using WebThamMyVien.API;
using WebThamMyVien.Interfaces;
using WebThamMyVien.Models;

namespace WebThamMyVien.Repository
{
    public class UnitOfWork: IUnitOfWork
    {
        public IAccountTypeRepository AccountType { get; set; }
        public IActionRepository Action { get; set; }
        public IActionTypeRepository ActionType { get; set; }
        public IAppointmentRepository Appointment { get; set; }
        public IAppointmentStatusRepository AppointmentStatus { get; set; }
        public IAppointmentTypeRepository AppointmentType { get; set; }
        public IBranchRepository Branch { get; set; }
        public ICustomerFollowUpRepository CustomerFollowUp { get; set; }
        public ICustomerHistoryRepository CustomerHistory { get; set; }
        public ICustomerImageRepository CustomerImage { get; set; }
        public ICustomerRepository Customer { get; set; }
        public ICustomerStatusRepository CustomerStatus { get; set; }
        public ICustomerTypeRepository CustomerType { get; set; }
        public IFollowUpDetailRepository FollowUpDetail { get; set; }
        public IFollowUpStatusRepository FollowUpStatus { get; set; }
        public IInvoiceDetailRepository InvoiceDetail { get; set; }
        public IInvoiceRepository Invoice { get; set; }
        public IInvoiceTypeRepository InvoiceType { get; set; }
        public IOnlinePaymentRepository OnlinePayment { get; set; }
        public IOrderDetailRepository OrderDetail { get; set; }
        public IOrderRepository Order { get; set; }
        public IOrderStatusRepository OrderStatus { get; set; }
        public IPaymentStatusRepository PaymentStatus { get; set; }
        public IPostRepository Post { get; set; }
        public IPostTypeRepository PostType { get; set; }
        public IProductBranchRepository ProductBranch { get; set; }
        public IProductImageRepository ProductImage { get; set; }
        public IProductRepository Product { get; set; }
        public IProductTypeRepository ProductType { get; set; }
        public IPromotionRepository Promotion { get; set; }
        public IServiceRepository Service { get; set; }
        public IServiceTypeRepository ServiceType { get; set; }
        public IShoppingCartItemRepository ShoppingCartItem { get; set; }
        public IShoppingCartRepository ShoppingCart { get; set; }
        public IUserAccountRepository UserAccount { get; set; }
        public IUserRepository User { get; set; }
        public IUserStatusRepository UserStatus { get; set; }
        public IUserTypeRepository UserType { get; set; }
        public IWarrantyReceiptRepository WarrantyReceipt { get; set; }
        public IWarrantyTypeRepository WarrantyType { get; set; }

        private readonly IOptions<ConnectAPI> _connectionStrings;
        private readonly HttpClient _httpClient;
        public UnitOfWork(HttpClient httpClient, IOptions<ConnectAPI> connectionStrings) {
            _httpClient = httpClient = new HttpClient();
            _connectionStrings = connectionStrings;

            AccountType = new AccountTypeRepository(_httpClient, _connectionStrings);
            Action = new ActionRepository(_httpClient, _connectionStrings);
            ActionType = new ActionTypeRepository(_httpClient, _connectionStrings);
            Appointment = new AppointmentRepository(_httpClient, _connectionStrings);
            AppointmentStatus = new AppointmentStatusRepository(_httpClient, _connectionStrings);
            AppointmentType = new AppointmentTypeRepository(_httpClient, _connectionStrings);
            Branch = new BranchRepository(_httpClient, _connectionStrings);
            CustomerFollowUp = new CustomerFollowUpRepository(_httpClient, _connectionStrings);
            CustomerHistory = new CustomerHistoryRepository(_httpClient, _connectionStrings);
            CustomerImage = new CustomerImageRepository(_httpClient, _connectionStrings);
            Customer = new CustomerRepository(_httpClient, _connectionStrings);
            CustomerStatus = new CustomerStatusRepository(_httpClient, _connectionStrings);
            CustomerType = new CustomerTypeRepository(_httpClient, _connectionStrings);
            FollowUpDetail = new FollowUpDetailRepository(_httpClient, _connectionStrings);
            FollowUpStatus = new FollowUpStatusRepository(_httpClient, _connectionStrings);
            InvoiceDetail = new InvoiceDetailRepository(_httpClient, _connectionStrings);
            Invoice = new InvoiceRepository(_httpClient, _connectionStrings);
            InvoiceType = new InvoiceTypeRepository(_httpClient, _connectionStrings);
            OnlinePayment = new OnlinePaymentRepository(_httpClient, _connectionStrings);
            OrderDetail = new OrderDetailRepository(_httpClient, _connectionStrings);
            Order = new OrderRepository(_httpClient, _connectionStrings);
            OrderStatus = new OrderStatusRepository(_httpClient, _connectionStrings);
            PaymentStatus = new PaymentStatusRepository(_httpClient, _connectionStrings);
            Post = new PostRepository(_httpClient, _connectionStrings);
            PostType = new PostTypeRepository(_httpClient, _connectionStrings);
            ProductBranch = new ProductBranchRepository(_httpClient, _connectionStrings);
            ProductImage = new ProductImageRepository(_httpClient, _connectionStrings);
            Product = new ProductRepository(_httpClient, _connectionStrings);
            ProductType = new ProductTypeRepository(_httpClient, _connectionStrings);
            Promotion = new PromotionRepository(_httpClient, _connectionStrings);
            Service = new ServiceRepository(_httpClient, _connectionStrings);
            ServiceType = new ServiceTypeRepository(_httpClient, _connectionStrings);
            ShoppingCartItem = new ShoppingCartItemRepository(_httpClient, _connectionStrings);
            ShoppingCart = new ShoppingCartRepository(_httpClient, _connectionStrings);
            UserAccount = new UserAccountRepository(_httpClient, _connectionStrings);
            User = new UserRepository(_httpClient, _connectionStrings);
            UserStatus = new UserStatusRepository(_httpClient, _connectionStrings);
            UserType = new UserTypeRepository(_httpClient, _connectionStrings);
            WarrantyReceipt = new WarrantyReceiptRepository(_httpClient, _connectionStrings);
            WarrantyType = new WarrantyTypeRepository(_httpClient, _connectionStrings);

        }
    }
}
