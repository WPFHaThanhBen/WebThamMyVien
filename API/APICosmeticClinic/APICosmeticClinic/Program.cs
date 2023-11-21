using APICosmeticClinic.Interfaces;
using APICosmeticClinic.Models;
using APICosmeticClinic.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<QL_CosmeticClinic_V2Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IAccountTypeRepository, AccountTypeRepository>();
builder.Services.AddScoped<IActionRepository, ActionRepository>();
builder.Services.AddScoped<IActionTypeRepository, ActionTypeRepository>();
builder.Services.AddScoped<IAppointmentRepository,AppointmentRepository>();
builder.Services.AddScoped<IAppointmentStatusRepository,AppointmentStatusRepository>();
builder.Services.AddScoped<IAppointmentTypeRepository,AppointmentTypeRepository>();
builder.Services.AddScoped<IBranchRepository,BranchRepository>();
builder.Services.AddScoped<ICustomerFollowUpRepository,CustomerFollowUpRepository>();
builder.Services.AddScoped<ICustomerHistoryRepository,CustomerHistoryRepository>();
builder.Services.AddScoped<ICustomerImageRepository,CustomerImageRepository>();
builder.Services.AddScoped<ICustomerRepository,CustomerRepository>();
builder.Services.AddScoped<ICustomerStatusRepository,CustomerStatusRepository>();
builder.Services.AddScoped<ICustomerTypeRepository,CustomerTypeRepository>();
builder.Services.AddScoped<IFollowUpDetailRepository,FollowUpDetailRepository>();
builder.Services.AddScoped<IFollowUpStatusRepository,FollowUpStatusRepository>();
builder.Services.AddScoped<IInvoiceDetailRepository,InvoiceDetailRepository>();
builder.Services.AddScoped<IInvoiceRepository,InvoiceRepository>();
builder.Services.AddScoped<IInvoiceTypeRepository,InvoiceTypeRepository>();
builder.Services.AddScoped<IOnlinePaymentRepository,OnlinePaymentRepository>();
builder.Services.AddScoped<IOrderDetailRepository,OrderDetailRepository>();
builder.Services.AddScoped<IOrderRepository,OrderRepository>();
builder.Services.AddScoped<IOrderStatusRepository,OrderStatusRepository>();
builder.Services.AddScoped<IPaymentStatusRepository,PaymentStatusRepository>();
builder.Services.AddScoped<IPostRepository,PostRepository>();
builder.Services.AddScoped<IPostTypeRepository,PostTypeRepository>();
builder.Services.AddScoped<IProductBranchRepository,ProductBranchRepository>();
builder.Services.AddScoped<IProductImageRepository,ProductImageRepository>();
builder.Services.AddScoped<IProductRepository,ProductRepository>();
builder.Services.AddScoped<IProductTypeRepository,ProductTypeRepository>();
builder.Services.AddScoped<IPromotionRepository,PromotionRepository>();
builder.Services.AddScoped<IServiceRepository,ServiceRepository>();
builder.Services.AddScoped<IServiceTypeRepository,ServiceTypeRepository>();
builder.Services.AddScoped<IShoppingCartItemRepository,ShoppingCartItemRepository>();
builder.Services.AddScoped<IShoppingCartRepository,ShoppingCartRepository>();
builder.Services.AddScoped<IUserAccountRepository,UserAccountRepository>();
builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddScoped<IUserStatusRepository,UserStatusRepository>();
builder.Services.AddScoped<IUserTypeRepository,UserTypeRepository>();
builder.Services.AddScoped<IWarrantyReceiptRepository,WarrantyReceiptRepository>();
builder.Services.AddScoped<IWarrantyTypeRepository,WarrantyTypeRepository>();











var app = builder.Build();







// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
