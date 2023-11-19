using EKO.ToDoApp.AppLogic.Services;
using EKO.ToDoApp.AppLogic.Services.Contracts;
using EKO.ToDoApp.Infrastructure.Storage;
using EKO.ToDoApp.Web.Filters;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateSlimBuilder(args);

builder.WebHost.ConfigureKestrel((context, options) =>
{
    options.ListenAnyIP(7064, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http1AndHttp2AndHttp3;
        listenOptions.UseHttps();
    });
});

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ToDoExceptionFilter>();
});

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<ToDoExceptionFilter>();
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromDays(3);
        options.SlidingExpiration = true;
        options.AccessDeniedPath = "/Error/";
    });

builder.WebHost.UseKestrelHttpsConfiguration();

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<ITaskService, TaskService>();
builder.Services.AddSingleton<ITaskListService, TaskListService>();

builder.Services.AddCors(options => options.AddPolicy("CorsPolicy", policy =>
{
    policy
        .AllowAnyHeader()
        .AllowAnyMethod()
        .SetIsOriginAllowed((_) => true)
        .AllowCredentials();
}));

builder.Services.AddDbContext<ToDoDbContext>(options =>
{
#if DEBUG
    options.UseSqlite(builder.Configuration.GetConnectionString("ToDoDev"));
#else
    options.UseSqlite(builder.Configuration.GetConnectionString("ToDoProd"));
#endif
});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();
app.UseStaticFiles();

var cookiePolicyOptions = new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
};

app.UseCookiePolicy(cookiePolicyOptions);

app.UseAuthentication();
app.UseAuthorization();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Tasks}/{action=Index}");

app.Run();
