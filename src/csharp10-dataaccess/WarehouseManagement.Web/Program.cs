using WarehouseManagement.Infra;
using WarehouseManagement.Infra.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Inject repositories
builder.Services.AddTransient<SqlServerContext>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IRepositoryBase<Customer>, RepositoryCustomer>();
builder.Services.AddTransient<IRepositoryBase<Item>, RepositoryItem>();
builder.Services.AddTransient<IRepositoryBase<Order>, RepositoryOrder>();
builder.Services.AddTransient<IRepositoryBase<ShippingProvider>, RepositoryShippingProvider>();
builder.Services.AddTransient<IRepositoryBase<Warehouse>, RepositoryWarehouse>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Order/Error");

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Order}/{action=Index}/{id?}");

app.Run();
