var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<Resunet.BL.Auth.IAuthBL, Resunet.BL.Auth.AuthBL>();
builder.Services.AddSingleton<Resunet.BL.Auth.IEncrypt, Resunet.BL.Auth.Encrypt>();
builder.Services.AddScoped<Resunet.BL.Auth.ICurrentUser, Resunet.BL.Auth.CurrentUser>(); // хранение состояния 
builder.Services.AddSingleton<Resunet.DAL.IAuthDAL, Resunet.DAL.AuthDAL>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// для сессии нужен дата провайдер
builder.Services.AddMvc().AddSessionStateTempDataProvider();
builder.Services.AddSession(); // включение сессии

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();