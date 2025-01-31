using ResunetBl.Auth;
using ResunetBl.General;
using ResunetBl.Profile;
using ResunetBl.Resume;
using ResunetDal.Implementations;
using ResunetDal.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IAuthDAL, AuthDAL>();
builder.Services.AddSingleton<IDbSessionDAL, DbSessionDAL>();
builder.Services.AddSingleton<IUserTokenDAL, UserTokenDAL>();
builder.Services.AddSingleton<IProfileDAL, ProfileDAL>();
builder.Services.AddSingleton<ISkillDAL, SkillDAL>();

builder.Services.AddScoped<ICurrentUser, CurrentUser>(); // хранение состояния 
builder.Services.AddScoped<IDbSession, DbSession>(); // хранение состояния 
builder.Services.AddScoped<IWebCookie, Resunet.Deps.WebCookie>();
builder.Services.AddTransient<IAuth, Auth>();
builder.Services.AddSingleton<IEncrypt, Resunet.Deps.Encrypt>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IProfile, Profile>();
builder.Services.AddSingleton<IResume, Resume>();
builder.Services.AddSingleton<ISkill, Skill>();

// для сессии нужен дата провайдер
builder.Services.AddMvc();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
    app.UseWebAssemblyDebugging();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseBlazorFrameworkFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

