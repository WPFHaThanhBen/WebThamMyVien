namespace WebThamMyVien.Interfaces
{
    public interface IUnitOfWork
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


    }
}
