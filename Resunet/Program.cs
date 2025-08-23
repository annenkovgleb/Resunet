using ResunetBl.Auth;
using ResunetBl.General;
using ResunetBl.Resume;
using ResunetDAL.Implementations;
using ResunetDAL.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<ResunetDAL.Interfaces.IAuth, ResunetDAL.Implementations.Auth>();
builder.Services.AddSingleton<ResunetDAL.Interfaces.IDbSession, ResunetDAL.Implementations.DbSession>();
builder.Services.AddSingleton<IUserToken, UserToken>();
builder.Services.AddSingleton<IProfile, Profile>();
builder.Services.AddSingleton<ISkill, Skill>();
builder.Services.AddSingleton<IPost, Post>();

builder.Services.AddScoped<ICurrentUser, CurrentUser>();
builder.Services.AddScoped<ResunetBl.Auth.IDbSession, ResunetBl.Auth.DbSession>();
builder.Services.AddScoped<IWebCookie, Resunet.Deps.WebCookie>();
builder.Services.AddTransient<ResunetBl.Auth.IAuth, ResunetBl.Auth.Auth>();
builder.Services.AddSingleton<IEncrypt, Resunet.Deps.Encrypt>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<ResunetBl.Profile.IProfile, ResunetBl.Profile.Profile>();
builder.Services.AddSingleton<IResume, Resume>();
builder.Services.AddSingleton<ResunetBl.Profile.ISkill, ResunetBl.Profile.Skill>();
builder.Services.AddSingleton<Resunet.Data.IPost, Resunet.Data.Post>();

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

