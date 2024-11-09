var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<Resunet.BL.Auth.IAuth, Resunet.BL.Auth.Auth>();
builder.Services.AddSingleton<Resunet.BL.Auth.IEncrypt, Resunet.BL.Auth.Encrypt>();
builder.Services.AddScoped<Resunet.BL.Auth.ICurrentUser, Resunet.BL.Auth.CurrentUser>(); // хранение состояния 
builder.Services.AddSingleton<Resunet.DAL.IAuthDAL, Resunet.DAL.AuthDAL>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<Resunet.DAL.IDbSessionDAL, Resunet.DAL.DbSessionDAL>();
builder.Services.AddScoped<Resunet.BL.Auth.IDbSession, Resunet.BL.Auth.DbSession>(); // хранение состояния 
builder.Services.AddScoped<Resunet.BL.General.IWebCookie, Resunet.BL.General.WebCookie>();

// для сессии нужен дата провайдер
builder.Services.AddMvc();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();