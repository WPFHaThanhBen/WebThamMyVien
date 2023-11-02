using AutoMapper;
using APICosmeticClinic.Dto;
using APICosmeticClinic.Models;
using Action = APICosmeticClinic.Models.Action;
using Microsoft.EntityFrameworkCore;

namespace APICosmeticClinic.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<AccountType, AccountTypeDto>();
            CreateMap<Action, ActionDto>();
            CreateMap<ActionType, ActionTypeDto>();
            CreateMap<Appointment, AppointmentDto>();
            CreateMap<AppointmentStatus, AppointmentStatusDto>();
            CreateMap<AppointmentType, AppointmentTypeDto>();
            CreateMap<Branch, BranchDto>();
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerFollowUp, CustomerFollowUpDto>();
            CreateMap<CustomerHistory, CustomerHistoryDto>();
            CreateMap<CustomerImage, CustomerImageDto>();
            CreateMap<CustomerStatus, CustomerStatusDto>();
            CreateMap<CustomerType, CustomerTypeDto>();
            CreateMap<FollowUpDetail, FollowUpDetailDto>();
            CreateMap<FollowUpStatus, FollowUpStatusDto>();
            CreateMap<Invoice, InvoiceDto>();
            CreateMap<InvoiceDetail, InvoiceDetailDto>();
            CreateMap<InvoiceType, InvoiceTypeDto>();
            CreateMap<OnlinePayment, OnlinePaymentDto>();
            CreateMap<Order, OrderDto>();
            CreateMap<OrderDetail, OrderDetailDto>();
            CreateMap<OrderStatus, OrderStatusDto>();
            CreateMap<PaymentStatus, PaymentStatusDto>();
            CreateMap<Post, PostDto>();
            CreateMap<PostContent, PostContentDto>();
            CreateMap<PostImage, PostImageDto>();
            CreateMap<PostType, PostTypeDto>();
            CreateMap<Product, ProductDto>();
            CreateMap<ProductBranch, ProductBranchDto>();
            CreateMap<ProductImage, ProductImageDto>();
            CreateMap<ProductType, ProductTypeDto>();
            CreateMap<Promotion, PromotionDto>();
            CreateMap<Service, ServiceDto>();
            CreateMap<ServiceType, ServiceTypeDto>();
            CreateMap<ShoppingCart, ShoppingCartDto>();
            CreateMap<ShoppingCartItem, ShoppingCartItemDto>();
            CreateMap<User, UserDto>();
            CreateMap<UserAccount, UserAccountDto>();
            CreateMap<UserStatus, UserStatusDto>();
            CreateMap<UserType, UserTypeDto>();
            CreateMap<WarrantyReceipt, WarrantyReceiptDto>();
            CreateMap<WarrantyType, WarrantyTypeDto>();

            CreateMap<AccountTypeDto, AccountType>();
            CreateMap<ActionDto, Action>();
            CreateMap<ActionTypeDto, ActionType>();
            CreateMap<AppointmentDto, Appointment>();
            CreateMap<AppointmentStatusDto, AppointmentStatus>();
            CreateMap<AppointmentTypeDto, AppointmentType>();
            CreateMap<BranchDto, Branch>();
            CreateMap<CustomerDto, Customer>();
            CreateMap<CustomerFollowUpDto, CustomerFollowUp>();
            CreateMap<CustomerHistoryDto, CustomerHistory>();
            CreateMap<CustomerImageDto, CustomerImage>();
            CreateMap<CustomerStatusDto, CustomerStatus>();
            CreateMap<CustomerTypeDto, CustomerType>();
            CreateMap<FollowUpDetailDto, FollowUpDetail>();
            CreateMap<FollowUpStatusDto, FollowUpStatus>();
            CreateMap<InvoiceDto, Invoice>();
            CreateMap<InvoiceDetailDto, InvoiceDetail>();
            CreateMap<InvoiceTypeDto, InvoiceType>();
            CreateMap<OnlinePaymentDto, OnlinePayment>();
            CreateMap<OrderDto, Order>();
            CreateMap<OrderDetailDto, OrderDetail>();
            CreateMap<OrderStatusDto, OrderStatus>();
            CreateMap<PaymentStatusDto, PaymentStatus>();
            CreateMap<PostDto, Post>();
            CreateMap<PostContentDto, PostContent>();
            CreateMap<PostImageDto, PostImage>();
            CreateMap<PostTypeDto, PostType>();
            CreateMap<ProductDto, Product>();
            CreateMap<ProductBranchDto, ProductBranch>();
            CreateMap<ProductImageDto, ProductImage>();
            CreateMap<ProductTypeDto, ProductType>();
            CreateMap<PromotionDto, Promotion>();
            CreateMap<ServiceDto, Service>();
            CreateMap<ServiceTypeDto, ServiceType>();
            CreateMap<ShoppingCartDto, ShoppingCart>();
            CreateMap<ShoppingCartItemDto, ShoppingCartItem>();
            CreateMap<UserDto, User>();
            CreateMap<UserAccountDto, UserAccount>();
            CreateMap<UserStatusDto, UserStatus>();
            CreateMap<UserTypeDto, UserType>();
            CreateMap<WarrantyReceiptDto, WarrantyReceipt>();
            CreateMap<WarrantyTypeDto, WarrantyType>();
        }
    }
}
