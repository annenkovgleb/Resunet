using ResunetBl.Auth;
using ResunetBl.General;
using ResunetBl.Resume;
using ResunetDAL.Implementations;
using ResunetDAL.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IAuthDAL, AuthDAL>();
builder.Services.AddSingleton<IDbSessionDAL, DbSessionDAL>();
builder.Services.AddSingleton<IUserTokenDAL, UserTokenDAL>();
builder.Services.AddSingleton<IProfileDAL, ProfileDAL>();
builder.Services.AddSingleton<ISkillDAL, SkillDAL>();
builder.Services.AddSingleton<IPostDAL, PostDAL>();

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

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseWebAssemblyDebugging();
}

ResunetDAL.DbHelper.ConnString = app.Configuration["ConnectionStrings:Default"] ?? "";

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseBlazorFrameworkFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

