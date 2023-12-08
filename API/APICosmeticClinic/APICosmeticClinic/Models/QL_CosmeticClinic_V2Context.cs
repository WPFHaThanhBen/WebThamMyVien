using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace APICosmeticClinic.Models
{
    public partial class QL_CosmeticClinic_V2Context : DbContext
    {
        public QL_CosmeticClinic_V2Context()
        {
        }

        public QL_CosmeticClinic_V2Context(DbContextOptions<QL_CosmeticClinic_V2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<AccountType> AccountTypes { get; set; } = null!;
        public virtual DbSet<Action> Actions { get; set; } = null!;
        public virtual DbSet<ActionType> ActionTypes { get; set; } = null!;
        public virtual DbSet<Appointment> Appointments { get; set; } = null!;
        public virtual DbSet<AppointmentStatus> AppointmentStatuses { get; set; } = null!;
        public virtual DbSet<AppointmentType> AppointmentTypes { get; set; } = null!;
        public virtual DbSet<Branch> Branches { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<CustomerFollowUp> CustomerFollowUps { get; set; } = null!;
        public virtual DbSet<CustomerHistory> CustomerHistories { get; set; } = null!;
        public virtual DbSet<CustomerImage> CustomerImages { get; set; } = null!;
        public virtual DbSet<CustomerStatus> CustomerStatuses { get; set; } = null!;
        public virtual DbSet<CustomerType> CustomerTypes { get; set; } = null!;
        public virtual DbSet<FollowUpDetail> FollowUpDetails { get; set; } = null!;
        public virtual DbSet<FollowUpStatus> FollowUpStatuses { get; set; } = null!;
        public virtual DbSet<Invoice> Invoices { get; set; } = null!;
        public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; } = null!;
        public virtual DbSet<InvoiceType> InvoiceTypes { get; set; } = null!;
        public virtual DbSet<OnlinePayment> OnlinePayments { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public virtual DbSet<OrderStatus> OrderStatuses { get; set; } = null!;
        public virtual DbSet<PaymentStatus> PaymentStatuses { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<PostType> PostTypes { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductBranch> ProductBranches { get; set; } = null!;
        public virtual DbSet<ProductImage> ProductImages { get; set; } = null!;
        public virtual DbSet<ProductType> ProductTypes { get; set; } = null!;
        public virtual DbSet<Promotion> Promotions { get; set; } = null!;
        public virtual DbSet<Service> Services { get; set; } = null!;
        public virtual DbSet<ServiceType> ServiceTypes { get; set; } = null!;
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; } = null!;
        public virtual DbSet<ShoppingCartItem> ShoppingCartItems { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserAccount> UserAccounts { get; set; } = null!;
        public virtual DbSet<UserStatus> UserStatuses { get; set; } = null!;
        public virtual DbSet<UserType> UserTypes { get; set; } = null!;
        public virtual DbSet<WarrantyReceipt> WarrantyReceipts { get; set; } = null!;
        public virtual DbSet<WarrantyType> WarrantyTypes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-125DL48\\SQLEXPRESS;Initial Catalog=QL_CosmeticClinic_V2;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountType>(entity =>
            {
                entity.ToTable("AccountType");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccountTypeName).HasMaxLength(255);

                entity.Property(e => e.DateDelete).HasMaxLength(20);

                entity.Property(e => e.Other).HasMaxLength(1000);
            });

            modelBuilder.Entity<Action>(entity =>
            {
                entity.ToTable("Action");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ActionTypeId).HasColumnName("ActionTypeID");

                entity.Property(e => e.ContentOfChange).HasMaxLength(1000);

                entity.Property(e => e.DateDelete).HasMaxLength(20);

                entity.Property(e => e.Time).HasMaxLength(40);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.ActionType)
                    .WithMany(p => p.Actions)
                    .HasForeignKey(d => d.ActionTypeId)
                    .HasConstraintName("FK__Action__ActionTy__4AB81AF0");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Actions)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Action__UserID__4BAC3F29");
            });

            modelBuilder.Entity<ActionType>(entity =>
            {
                entity.ToTable("ActionType");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateDelete).HasMaxLength(20);

                entity.Property(e => e.TypeName).HasMaxLength(255);
            });

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.ToTable("Appointment");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AppointmentDate).HasMaxLength(20);

                entity.Property(e => e.AppointmentStatusId).HasColumnName("AppointmentStatusID");

                entity.Property(e => e.AppointmentTypeId).HasColumnName("AppointmentTypeID");

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreationDate).HasMaxLength(20);

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.DateDelete).HasMaxLength(20);

                entity.Property(e => e.Other).HasMaxLength(255);

                entity.Property(e => e.ReceivedByUserId).HasColumnName("ReceivedByUserID");

                entity.Property(e => e.ServicePerformedId).HasColumnName("ServicePerformedID");

                entity.HasOne(d => d.AppointmentStatus)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.AppointmentStatusId)
                    .HasConstraintName("FK__Appointme__Appoi__04E4BC85");

                entity.HasOne(d => d.AppointmentType)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.AppointmentTypeId)
                    .HasConstraintName("FK__Appointme__Appoi__03F0984C");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK__Appointme__Branc__02FC7413");

                entity.HasOne(d => d.CreatedByUser)
                    .WithMany(p => p.AppointmentCreatedByUsers)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .HasConstraintName("FK__Appointme__Creat__7F2BE32F");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__Appointme__Custo__01142BA1");

                entity.HasOne(d => d.ReceivedByUser)
                    .WithMany(p => p.AppointmentReceivedByUsers)
                    .HasForeignKey(d => d.ReceivedByUserId)
                    .HasConstraintName("FK__Appointme__Recei__00200768");

                entity.HasOne(d => d.ServicePerformed)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.ServicePerformedId)
                    .HasConstraintName("FK__Appointme__Servi__02084FDA");
            });

            modelBuilder.Entity<AppointmentStatus>(entity =>
            {
                entity.ToTable("AppointmentStatus");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateDelete).HasMaxLength(20);

                entity.Property(e => e.Other).HasMaxLength(1000);

                entity.Property(e => e.StatusName).HasMaxLength(255);
            });

            modelBuilder.Entity<AppointmentType>(entity =>
            {
                entity.ToTable("AppointmentType");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateDelete).HasMaxLength(20);

                entity.Property(e => e.Other).HasMaxLength(1000);

                entity.Property(e => e.TypeName).HasMaxLength(255);
            });

            modelBuilder.Entity<Branch>(entity =>
            {
                entity.ToTable("Branch");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.DateDelete).HasMaxLength(20);

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Phone).HasMaxLength(11);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.Birthdate).HasMaxLength(20);

                entity.Property(e => e.CustomerStatusId).HasColumnName("CustomerStatusID");

                entity.Property(e => e.CustomerTypeId).HasColumnName("CustomerTypeID");

                entity.Property(e => e.DateDelete).HasMaxLength(20);

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.FacebookLink).HasMaxLength(255);

                entity.Property(e => e.FullName).HasMaxLength(255);

                entity.Property(e => e.Gender).HasMaxLength(20);

                entity.Property(e => e.Idcard)
                    .HasMaxLength(255)
                    .HasColumnName("IDCard");

                entity.Property(e => e.InterestedServices).HasMaxLength(1000);

                entity.Property(e => e.Other).HasMaxLength(1000);

                entity.Property(e => e.PhoneNumber).HasMaxLength(255);

                entity.Property(e => e.ZaloLink).HasMaxLength(255);

                entity.HasOne(d => d.CustomerStatus)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.CustomerStatusId)
                    .HasConstraintName("FK__Customer__DateDe__52593CB8");

                entity.HasOne(d => d.CustomerType)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.CustomerTypeId)
                    .HasConstraintName("FK__Customer__Custom__534D60F1");
            });

            modelBuilder.Entity<CustomerFollowUp>(entity =>
            {
                entity.ToTable("CustomerFollowUp");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.DateDelete).HasMaxLength(20);

                entity.Property(e => e.FollowUpBranchId).HasColumnName("FollowUpBranchID");

                entity.Property(e => e.FollowUpStatusId).HasColumnName("FollowUpStatusID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerFollowUps)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__CustomerF__Custo__72C60C4A");

                entity.HasOne(d => d.FollowUpBranch)
                    .WithMany(p => p.CustomerFollowUps)
                    .HasForeignKey(d => d.FollowUpBranchId)
                    .HasConstraintName("FK__CustomerF__Follo__75A278F5");

                entity.HasOne(d => d.FollowUpStatus)
                    .WithMany(p => p.CustomerFollowUps)
                    .HasForeignKey(d => d.FollowUpStatusId)
                    .HasConstraintName("FK__CustomerF__Follo__74AE54BC");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CustomerFollowUps)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__CustomerF__UserI__73BA3083");
            });

            modelBuilder.Entity<CustomerHistory>(entity =>
            {
                entity.ToTable("CustomerHistory");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ConsultingUserId).HasColumnName("ConsultingUserID");

                entity.Property(e => e.CustomerCareUserId).HasColumnName("CustomerCareUserID");

                entity.Property(e => e.DateDelete).HasMaxLength(20);

                entity.Property(e => e.InvoiceId).HasColumnName("InvoiceID");

                entity.Property(e => e.Other).HasMaxLength(1000);

                entity.Property(e => e.ServiceBranchId).HasColumnName("ServiceBranchID");

                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

                entity.Property(e => e.TechnicalUserId).HasColumnName("TechnicalUserID");

                entity.Property(e => e.UsageDate).HasMaxLength(20);

                entity.Property(e => e.WarrantyId).HasColumnName("WarrantyID");

                entity.HasOne(d => d.ConsultingUser)
                    .WithMany(p => p.CustomerHistoryConsultingUsers)
                    .HasForeignKey(d => d.ConsultingUserId)
                    .HasConstraintName("FK__CustomerH__Consu__3B40CD36");

                entity.HasOne(d => d.CustomerCareUser)
                    .WithMany(p => p.CustomerHistoryCustomerCareUsers)
                    .HasForeignKey(d => d.CustomerCareUserId)
                    .HasConstraintName("FK__CustomerH__Custo__3C34F16F");

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.CustomerHistories)
                    .HasForeignKey(d => d.InvoiceId)
                    .HasConstraintName("FK__CustomerH__Invoi__3A4CA8FD");

                entity.HasOne(d => d.ServiceBranch)
                    .WithMany(p => p.CustomerHistories)
                    .HasForeignKey(d => d.ServiceBranchId)
                    .HasConstraintName("FK__CustomerH__Servi__3E1D39E1");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.CustomerHistories)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("FK__CustomerH__Servi__3864608B");

                entity.HasOne(d => d.TechnicalUser)
                    .WithMany(p => p.CustomerHistoryTechnicalUsers)
                    .HasForeignKey(d => d.TechnicalUserId)
                    .HasConstraintName("FK__CustomerH__Techn__3D2915A8");

                entity.HasOne(d => d.Warranty)
                    .WithMany(p => p.CustomerHistories)
                    .HasForeignKey(d => d.WarrantyId)
                    .HasConstraintName("FK__CustomerH__Warra__395884C4");
            });

            modelBuilder.Entity<CustomerImage>(entity =>
            {
                entity.ToTable("CustomerImage");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.DateDelete).HasMaxLength(20);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerImages)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__CustomerI__Custo__17F790F9");
            });

            modelBuilder.Entity<CustomerStatus>(entity =>
            {
                entity.ToTable("CustomerStatus");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateDelete).HasMaxLength(20);

                entity.Property(e => e.Other).HasMaxLength(1000);

                entity.Property(e => e.StatusName).HasMaxLength(255);
            });

            modelBuilder.Entity<CustomerType>(entity =>
            {
                entity.ToTable("CustomerType");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateDelete).HasMaxLength(20);

                entity.Property(e => e.Other).HasMaxLength(1000);

                entity.Property(e => e.TypeName).HasMaxLength(255);
            });

            modelBuilder.Entity<FollowUpDetail>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateDelete).HasMaxLength(20);

                entity.Property(e => e.Details).HasMaxLength(1000);

                entity.Property(e => e.FollowUpDate).HasMaxLength(20);

                entity.Property(e => e.FollowUpReceiptId).HasColumnName("FollowUpReceiptID");

                entity.HasOne(d => d.FollowUpReceipt)
                    .WithMany(p => p.FollowUpDetails)
                    .HasForeignKey(d => d.FollowUpReceiptId)
                    .HasConstraintName("FK__FollowUpD__Follo__787EE5A0");
            });

            modelBuilder.Entity<FollowUpStatus>(entity =>
            {
                entity.ToTable("FollowUpStatus");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateDelete).HasMaxLength(20);

                entity.Property(e => e.Other).HasMaxLength(1000);

                entity.Property(e => e.StatusName).HasMaxLength(255);
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.ToTable("Invoice");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.DateDelete).HasMaxLength(20);

                entity.Property(e => e.InvoiceDate).HasMaxLength(20);

                entity.Property(e => e.InvoiceTypeId).HasColumnName("InvoiceTypeID");

                entity.Property(e => e.Other).HasMaxLength(1000);

                entity.Property(e => e.PaymentMethod).HasMaxLength(255);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK__Invoice__BranchI__2B0A656D");

                entity.HasOne(d => d.CreatedByUser)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .HasConstraintName("FK__Invoice__Created__29221CFB");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__Invoice__Custome__2A164134");

                entity.HasOne(d => d.InvoiceType)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.InvoiceTypeId)
                    .HasConstraintName("FK__Invoice__Invoice__282DF8C2");
            });

            modelBuilder.Entity<InvoiceDetail>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Content).HasMaxLength(255);

                entity.Property(e => e.DateDelete).HasMaxLength(20);

                entity.Property(e => e.InvoiceId).HasColumnName("InvoiceID");

                entity.HasOne(d => d.DiscountNavigation)
                    .WithMany(p => p.InvoiceDetails)
                    .HasForeignKey(d => d.Discount)
                    .HasConstraintName("FK__InvoiceDe__Disco__2EDAF651");

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.InvoiceDetails)
                    .HasForeignKey(d => d.InvoiceId)
                    .HasConstraintName("FK__InvoiceDe__Invoi__2DE6D218");
            });

            modelBuilder.Entity<InvoiceType>(entity =>
            {
                entity.ToTable("InvoiceType");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateDelete).HasMaxLength(20);

                entity.Property(e => e.InvoiceTypeName).HasMaxLength(255);

                entity.Property(e => e.Other).HasMaxLength(1000);
            });

            modelBuilder.Entity<OnlinePayment>(entity =>
            {
                entity.ToTable("OnlinePayment");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.DateDelete).HasMaxLength(20);

                entity.Property(e => e.InvoiceId).HasColumnName("InvoiceID");

                entity.Property(e => e.PaymentDate).HasMaxLength(20);

                entity.Property(e => e.PaymentDescription).HasMaxLength(255);

                entity.Property(e => e.PaymentStatusId).HasColumnName("PaymentStatusID");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.OnlinePayments)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK__OnlinePay__Branc__3587F3E0");

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.OnlinePayments)
                    .HasForeignKey(d => d.InvoiceId)
                    .HasConstraintName("FK__OnlinePay__Invoi__339FAB6E");

                entity.HasOne(d => d.PaymentStatus)
                    .WithMany(p => p.OnlinePayments)
                    .HasForeignKey(d => d.PaymentStatusId)
                    .HasConstraintName("FK__OnlinePay__Payme__3493CFA7");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.DateDelete).HasMaxLength(20);

                entity.Property(e => e.DeliveryAddress).HasMaxLength(255);

                entity.Property(e => e.OrderDate).HasMaxLength(20);

                entity.Property(e => e.OrderStatusId).HasColumnName("OrderStatusID");

                entity.Property(e => e.Other).HasMaxLength(1000);

                entity.Property(e => e.RecipientPhoneNumber).HasMaxLength(20);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK__Order__BranchID__1DB06A4F");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__Order__CustomerI__1EA48E88");

                entity.HasOne(d => d.OrderStatus)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.OrderStatusId)
                    .HasConstraintName("FK__Order__OrderStat__1CBC4616");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateDelete).HasMaxLength(20);

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.PromotionId).HasColumnName("PromotionID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__OrderDeta__Order__2180FB33");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__OrderDeta__Produ__22751F6C");

                entity.HasOne(d => d.Promotion)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.PromotionId)
                    .HasConstraintName("FK__OrderDeta__Promo__236943A5");
            });

            modelBuilder.Entity<OrderStatus>(entity =>
            {
                entity.ToTable("OrderStatus");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateDelete).HasMaxLength(20);

                entity.Property(e => e.StatusName).HasMaxLength(255);
            });

            modelBuilder.Entity<PaymentStatus>(entity =>
            {
                entity.ToTable("PaymentStatus");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateDelete).HasMaxLength(20);

                entity.Property(e => e.StatusName).HasMaxLength(255);
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateDelete).HasMaxLength(20);

                entity.Property(e => e.Introduce).HasMaxLength(2000);

                entity.Property(e => e.PostTypeId).HasColumnName("PostTypeID");

                entity.Property(e => e.PostedByUserId).HasColumnName("PostedByUserID");

                entity.Property(e => e.PostingDateCreate).HasMaxLength(20);

                entity.Property(e => e.PostingDateUpdate).HasMaxLength(20);

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.HasOne(d => d.PostType)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.PostTypeId)
                    .HasConstraintName("FK__Post__DateDelete__114A936A");

                entity.HasOne(d => d.PostedByUser)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.PostedByUserId)
                    .HasConstraintName("FK__Post__PostedByUs__123EB7A3");
            });

            modelBuilder.Entity<PostType>(entity =>
            {
                entity.ToTable("PostType");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateDelete).HasMaxLength(20);

                entity.Property(e => e.Other).HasMaxLength(1000);

                entity.Property(e => e.TypeName).HasMaxLength(255);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AppliedPromotionId).HasColumnName("AppliedPromotionID");

                entity.Property(e => e.DateDelete).HasMaxLength(20);

                entity.Property(e => e.Functionality).HasMaxLength(1500);

                entity.Property(e => e.Introduction).HasMaxLength(1500);

                entity.Property(e => e.Origin).HasMaxLength(255);

                entity.Property(e => e.Other).HasMaxLength(1000);

                entity.Property(e => e.ProductName).HasMaxLength(255);

                entity.Property(e => e.ProductTypeId).HasColumnName("ProductTypeID");

                entity.HasOne(d => d.AppliedPromotion)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.AppliedPromotionId)
                    .HasConstraintName("FK__Product__Applied__5FB337D6");

                entity.HasOne(d => d.ProductType)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductTypeId)
                    .HasConstraintName("FK__Product__Product__60A75C0F");
            });

            modelBuilder.Entity<ProductBranch>(entity =>
            {
                entity.ToTable("Product_Branch");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.DateDelete).HasMaxLength(20);

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.ProductBranches)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK__Product_B__Branc__6477ECF3");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductBranches)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__Product_B__Produ__6383C8BA");
            });

            modelBuilder.Entity<ProductImage>(entity =>
            {
                entity.ToTable("ProductImage");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateDelete).HasMaxLength(20);

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductImages)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__ProductIm__Produ__151B244E");
            });

            modelBuilder.Entity<ProductType>(entity =>
            {
                entity.ToTable("ProductType");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateDelete).HasMaxLength(20);

                entity.Property(e => e.Other).HasMaxLength(1000);

                entity.Property(e => e.ProductTypeName).HasMaxLength(255);
            });

            modelBuilder.Entity<Promotion>(entity =>
            {
                entity.ToTable("Promotion");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateDelete).HasMaxLength(20);

                entity.Property(e => e.Other).HasMaxLength(1000);

                entity.Property(e => e.PromotionName).HasMaxLength(255);

                entity.Property(e => e.PromotionTime).HasMaxLength(20);

                entity.Property(e => e.PromotionValue).HasMaxLength(255);
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.ToTable("Service");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AppliedPromotionId).HasColumnName("AppliedPromotionID");

                entity.Property(e => e.DateDelete).HasMaxLength(20);

                entity.Property(e => e.Other).HasMaxLength(1000);

                entity.Property(e => e.ServiceDetails).HasMaxLength(2000);

                entity.Property(e => e.ServiceName).HasMaxLength(255);

                entity.Property(e => e.ServiceTypeId).HasColumnName("ServiceTypeID");

                entity.HasOne(d => d.AppliedPromotion)
                    .WithMany(p => p.Services)
                    .HasForeignKey(d => d.AppliedPromotionId)
                    .HasConstraintName("FK__Service__Applied__5AEE82B9");

                entity.HasOne(d => d.ServiceType)
                    .WithMany(p => p.Services)
                    .HasForeignKey(d => d.ServiceTypeId)
                    .HasConstraintName("FK__Service__Service__59FA5E80");
            });

            modelBuilder.Entity<ServiceType>(entity =>
            {
                entity.ToTable("ServiceType");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateDelete).HasMaxLength(20);

                entity.Property(e => e.Other).HasMaxLength(1000);

                entity.Property(e => e.TypeName).HasMaxLength(255);
            });

            modelBuilder.Entity<ShoppingCart>(entity =>
            {
                entity.ToTable("ShoppingCart");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateDelete).HasMaxLength(20);

                entity.Property(e => e.UserAccountId).HasColumnName("UserAccountID");

                entity.HasOne(d => d.UserAccount)
                    .WithMany(p => p.ShoppingCarts)
                    .HasForeignKey(d => d.UserAccountId)
                    .HasConstraintName("FK__ShoppingC__UserA__07C12930");
            });

            modelBuilder.Entity<ShoppingCartItem>(entity =>
            {
                entity.ToTable("ShoppingCartItem");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateDelete).HasMaxLength(20);

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

                entity.Property(e => e.ShoppingCartId).HasColumnName("ShoppingCartID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ShoppingCartItems)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__ShoppingC__Produ__0B91BA14");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.ShoppingCartItems)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("FK__ShoppingC__Servi__0C85DE4D");

                entity.HasOne(d => d.ShoppingCart)
                    .WithMany(p => p.ShoppingCartItems)
                    .HasForeignKey(d => d.ShoppingCartId)
                    .HasConstraintName("FK__ShoppingC__Shopp__0A9D95DB");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BankAccount).HasMaxLength(20);

                entity.Property(e => e.Birthdate).HasMaxLength(255);

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CurrentAddress).HasMaxLength(255);

                entity.Property(e => e.DateDelete).HasMaxLength(20);

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.FullName).HasMaxLength(255);

                entity.Property(e => e.Gender).HasMaxLength(20);

                entity.Property(e => e.HealthInsurance).HasMaxLength(20);

                entity.Property(e => e.Hometown).HasMaxLength(255);

                entity.Property(e => e.Idcard)
                    .HasMaxLength(20)
                    .HasColumnName("IDCard");

                entity.Property(e => e.Other).HasMaxLength(1000);

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.Property(e => e.SocialInsurance).HasMaxLength(20);

                entity.Property(e => e.UserStatusId).HasColumnName("UserStatusID");

                entity.Property(e => e.UserTypeId).HasColumnName("UserTypeID");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK__User__BranchID__3F466844");

                entity.HasOne(d => d.UserStatus)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserStatusId)
                    .HasConstraintName("FK__User__UserStatus__3E52440B");

                entity.HasOne(d => d.UserType)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserTypeId)
                    .HasConstraintName("FK__User__UserTypeID__3D5E1FD2");
            });

            modelBuilder.Entity<UserAccount>(entity =>
            {
                entity.ToTable("UserAccount");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccountTypeId).HasColumnName("AccountTypeID");

                entity.Property(e => e.DateDelete).HasMaxLength(20);

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.Password).HasMaxLength(255);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.UserStatusId).HasColumnName("UserStatusID");

                entity.Property(e => e.Username).HasMaxLength(255);

                entity.HasOne(d => d.AccountType)
                    .WithMany(p => p.UserAccounts)
                    .HasForeignKey(d => d.AccountTypeId)
                    .HasConstraintName("FK__UserAccou__Accou__45F365D3");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.UserAccounts)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_UserAccount_Customer");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserAccounts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__UserAccou__UserI__44FF419A");

                entity.HasOne(d => d.UserStatus)
                    .WithMany(p => p.UserAccounts)
                    .HasForeignKey(d => d.UserStatusId)
                    .HasConstraintName("FK__UserAccou__UserS__440B1D61");
            });

            modelBuilder.Entity<UserStatus>(entity =>
            {
                entity.ToTable("UserStatus");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateDelete).HasMaxLength(20);

                entity.Property(e => e.Other).HasMaxLength(1000);

                entity.Property(e => e.StatusName).HasMaxLength(100);
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.ToTable("UserType");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateDelete).HasMaxLength(20);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Other).HasMaxLength(1000);
            });

            modelBuilder.Entity<WarrantyReceipt>(entity =>
            {
                entity.ToTable("WarrantyReceipt");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.DateDelete).HasMaxLength(20);

                entity.Property(e => e.FollowUpUserId).HasColumnName("FollowUpUserID");

                entity.Property(e => e.HandledAtBranchId).HasColumnName("HandledAtBranchID");

                entity.Property(e => e.Other).HasMaxLength(1000);

                entity.Property(e => e.ServicedServiceId).HasColumnName("ServicedServiceID");

                entity.Property(e => e.WarrantyDate).HasMaxLength(20);

                entity.Property(e => e.WarrantyDetails).HasMaxLength(1500);

                entity.Property(e => e.WarrantyEndDate).HasMaxLength(20);

                entity.Property(e => e.WarrantyHandledByUserId).HasColumnName("WarrantyHandledByUserID");

                entity.Property(e => e.WarrantyTypeId).HasColumnName("WarrantyTypeID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.WarrantyReceipts)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__WarrantyR__Custo__6B24EA82");

                entity.HasOne(d => d.FollowUpUser)
                    .WithMany(p => p.WarrantyReceiptFollowUpUsers)
                    .HasForeignKey(d => d.FollowUpUserId)
                    .HasConstraintName("FK__WarrantyR__Follo__6A30C649");

                entity.HasOne(d => d.HandledAtBranch)
                    .WithMany(p => p.WarrantyReceipts)
                    .HasForeignKey(d => d.HandledAtBranchId)
                    .HasConstraintName("FK__WarrantyR__Handl__6E01572D");

                entity.HasOne(d => d.ServicedService)
                    .WithMany(p => p.WarrantyReceipts)
                    .HasForeignKey(d => d.ServicedServiceId)
                    .HasConstraintName("FK__WarrantyR__Servi__6C190EBB");

                entity.HasOne(d => d.WarrantyHandledByUser)
                    .WithMany(p => p.WarrantyReceiptWarrantyHandledByUsers)
                    .HasForeignKey(d => d.WarrantyHandledByUserId)
                    .HasConstraintName("FK__WarrantyR__Warra__693CA210");

                entity.HasOne(d => d.WarrantyType)
                    .WithMany(p => p.WarrantyReceipts)
                    .HasForeignKey(d => d.WarrantyTypeId)
                    .HasConstraintName("FK__WarrantyR__Warra__6D0D32F4");
            });

            modelBuilder.Entity<WarrantyType>(entity =>
            {
                entity.ToTable("WarrantyType");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateDelete).HasMaxLength(20);

                entity.Property(e => e.Other).HasMaxLength(1000);

                entity.Property(e => e.WarrantyTypeName).HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
