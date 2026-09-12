using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPR521_Shop;
using SPR521_Shop.Initializer;
using SPR521_Shop.Models;
using SPR521_Shop.Repositories;
using SPR521_Shop.Services;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

// Add dbContext
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    var connectionString = builder.Configuration.GetConnectionString("localDb");
    opt.UseNpgsql(connectionString);
});

// Add session
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(cfg =>
{
    cfg.Cookie.HttpOnly = true;
    cfg.Cookie.IsEssential = true;
    cfg.IdleTimeout = TimeSpan.FromHours(1);
});

// Add identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;

    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    options.Lockout.AllowedForNewUsers = true;
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();

// Add Repositories to Dependency injection
//  лас буде ≥снувати в одному екземпл€р≥
//builder.Services.AddSingleton<CategoryRepository>();

// Ѕуде створювати екземпл€р кожного разу коли в≥н потр≥бен
//builder.Services.AddTransient<CategoryRepository>();

// —творюЇ екземпл€р коли приходить запит та видал€Ї коли в≥дправл€Їтьс€ в≥дпов≥дь
builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<ProductRepository>();
builder.Services.AddScoped<CartRepository>();

// Add services
builder.Services.AddScoped<ImageService>();
builder.Services.AddScoped<CartService>();
//builder.Services.AddScoped<IEmailSender, EmailService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapStaticAssets();

// ¬иконуЇтьс€ коли користувач заходить на сайт
app.Use(async (context, next) =>
{
    if (context.User.Identity != null && context.User.Identity.IsAuthenticated)
    {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId != null)
        {
            var cartService = context.RequestServices.GetRequiredService<CartService>();
            var cartRepository = context.RequestServices.GetRequiredService<CartRepository>();
            if(cartService.Count() != await cartRepository.CountAsync(userId))
            {
                var userItems = await cartRepository.GetItemsAsync(userId);

                foreach (var item in userItems)
                {
                    cartService.Add(item.ProductId, item.Count);
                }
            }
        }
    }

    await next();
});

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// ¬иклик нашого seed
app.Seed();


app.Run();