using Microsoft.EntityFrameworkCore;
using SPR521_Shop;
using SPR521_Shop.Controllers;
using SPR521_Shop.Initializer;
using SPR521_Shop.Repositories;
using SPR521_Shop.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add dbContext
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    var connectionString = builder.Configuration.GetConnectionString("localDb");
    opt.UseNpgsql(connectionString);
});

// Add Repositories to Dependency injection
//  лас буде ≥снувати в одному екземпл€р≥
//builder.Services.AddSingleton<CategoryRepository>();

// Ѕуде створювати екземпл€р кожного разу коли в≥н потр≥бен
//builder.Services.AddTransient<CategoryRepository>();

// —творюЇ екземпл€р коли приходить запит та видал€Ї коли в≥дправл€Їтьс€ в≥дпов≥дь
builder.Services.AddScoped<CategoryRepository>();

// Add services
builder.Services.AddScoped<ImageService>();

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

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// ¬иклик нашого seed
app.Seed();


app.Run();