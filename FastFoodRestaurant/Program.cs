using FastFoodRestaurant.Data;
using Microsoft.EntityFrameworkCore;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FastFoodRestaurant.Repository.IGenericRepository;
using FastFoodRestaurant.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IMenuRepository,MenuRepository>();
builder.Services.AddScoped<IComboRepository,ComboRepository>();
builder.Services.AddScoped<IVoucherRepository, VoucherRepository>();
builder.Services.AddScoped<IStoreRepository, StoreRepository>();
builder.Services.AddScoped<IPartyRepository, PartyRepository>();
builder.Services.AddScoped<ISystemUserRepository, SystemUserRepository>();
builder.Services.AddScoped<IBillRepository, BillRepository>();
builder.Services.AddScoped<IComboDetailRepository, ComboDetailRepository>();

// Set your Cloudinary credentials
//=================================

var cloudinaryUrl = builder.Configuration["Cloudinary:CLOUDINARY_URL"];
Cloudinary cloudinary = new Cloudinary(cloudinaryUrl);
cloudinary.Api.Secure = true;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "",
    defaults: new { area = "Admin", controller = "Home", action = "Index" });

app.MapControllerRoute(
    name: "Admin_Home",
    pattern: "admin/trang-chu",
    defaults: new {area = "Admin",controller="Home",action="Index"});

app.MapControllerRoute(
    name: "Admin_Login",
    pattern: "admin/dang-nhap",
    defaults: new { area = "Admin", controller = "Authentication", action = "Login" });

app.MapControllerRoute(
    name: "Admin_Register",
    pattern: "admin/dang-ky",
    defaults: new { area = "Admin", controller = "Authentication", action = "Register" });

app.MapControllerRoute(
    name: "Admin_Bill",
    pattern: "admin/don-hang",
    defaults: new { area = "Admin", controller = "Bill", action = "Index" });

app.MapControllerRoute(
    name: "Admin_Category",
    pattern: "admin/danh-muc",
    defaults: new { area = "Admin", controller = "Category", action = "Index" });

app.MapControllerRoute(
    name: "Admin_Category_Create",
    pattern: "admin/danh-muc/them",
    defaults: new { area = "Admin", controller = "Category", action = "Create" });

app.MapControllerRoute(
    name: "Admin_Category_Edit",
    pattern: "admin/danh-muc/chinh-sua/{id}",
    defaults: new { area = "Admin", controller = "Category", action = "Edit" });

app.MapControllerRoute(
    name: "Admin_Combo",
    pattern: "admin/uu-dai",
    defaults: new { area = "Admin", controller = "Combo", action = "Index" });

app.MapControllerRoute(
    name: "Admin_Menu",
    pattern: "admin/thuc-don",
    defaults: new { area = "Admin", controller = "Menu", action = "Index" });

app.MapControllerRoute(
    name: "Admin_Menu-Create",
    pattern: "admin/thuc-don/them",
    defaults: new { area = "Admin", controller = "Menu", action = "Create" });

app.MapControllerRoute(
    name: "Admin_Menu_Edit",
    pattern: "admin/thuc-don/chinh-sua/${id}",
    defaults: new { area = "Admin", controller = "Menu", action = "Edit" });

app.MapControllerRoute(
    name: "Admin_Party",
    pattern: "admin/tiec",
    defaults: new { area = "Admin", controller = "Party", action = "Index" });

app.MapControllerRoute(
    name: "Admin_Store",
    pattern: "admin/chi-nhanh",
    defaults: new { area = "Admin", controller = "Store", action = "Index" });

app.MapControllerRoute(
    name: "Admin_Store_Create",
    pattern: "admin/chi-nhanh/them",
    defaults: new { area = "Admin", controller = "Store", action = "Create" });

app.MapControllerRoute(
    name: "Admin_Store_Edit",
    pattern: "admin/chi-nhanh/chinh-sua/${id}",
    defaults: new { area = "Admin", controller = "Store", action = "Edit" });

app.MapControllerRoute(
    name: "Admin_SystemUser",
    pattern: "admin/nhan-vien",
    defaults: new { area = "Admin", controller = "SystemUser", action = "Index" });

app.MapControllerRoute(
    name: "Admin_Voucher",
    pattern: "admin/voucher",
    defaults: new { area = "Admin", controller = "Voucher", action = "Index" });

app.MapControllerRoute(
    name: "Admin_Voucher_Create",
    pattern: "admin/voucher/them",
    defaults: new { area = "Admin", controller = "Voucher", action = "Create" });

app.MapControllerRoute(
    name: "Admin_Voucher_Edit",
    pattern: "admin/voucher/chinh-sua/${id}",
    defaults: new { area = "Admin", controller = "Voucher", action = "Edit" });

app.MapControllerRoute(
    name: "Client_Cart",
    pattern: "gio-hang",
    defaults: new { area = "Client", controller = "Cart", action = "Index" });

app.MapControllerRoute(
    name: "Client_Home",
    pattern: "trang-chu",
    defaults: new { area = "Client", controller = "Home", action = "Index" });

app.MapControllerRoute(
    name: "Client_Menu",
    pattern: "thuc-don",
    defaults: new { area = "Client", controller = "Menu", action = "Index" });

app.MapControllerRoute(
    name: "Client_Party",
    pattern: "dat-tiec",
    defaults: new { area = "Client", controller = "Party", action = "Index" });

app.MapControllerRoute(
    name: "Client_Promotion",
    pattern: "uu-dai",
    defaults: new { area = "Client", controller = "Promotion", action = "Index" });

app.MapControllerRoute(
    name: "Client_Store",
    pattern: "chi-nhanh",
    defaults: new { area = "Client", controller = "Store", action = "Index" });



app.Run();
