using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Screens.data;
using Screens.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<Screens.data.AppDbContext>(options =>
    options.UseSqlServer(
        //"Data Source=localdb\\MSSQLLocalDB;Initial Catalog=test;User ID=arsaqqa;Password=123456;TrustServerCertificate=True"
        builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("ERROR : NO Connection String")
  )
);
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Allow numeric-only passwords
    options.Password.RequireDigit = false;          // no digit requirement
    options.Password.RequireLowercase = false;      // no lowercase requirement
    options.Password.RequireUppercase = false;      // no uppercase requirement
    options.Password.RequireNonAlphanumeric = false;// no special chars requirement
    options.Password.RequiredLength = 4;            // minimum length (set as needed)
    options.Password.RequiredUniqueChars = 1;       // only 1 unique char required
});


// Configure application cookie to redirect unauthenticated users to Account/Login
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login"; // redirect here when not authenticated
    options.AccessDeniedPath = "/Account/AccessDenied"; // optional: access denied
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
});

var app = builder.Build();
//using (var scope = app.Services.CreateScope())
//{
//    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
//    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

//    string[] roles = { "Admin", "User" };

//    // ≈‰‘«¡ Roles
//    foreach (var role in roles)
//    {
//        if (!await roleManager.RoleExistsAsync(role))
//        {
//            await roleManager.CreateAsync(new IdentityRole(role));
//        }
//    }

//    // ≈‰‘«¡ Admin User
//    var adminEmail = "admin@test.com";
//    var adminPassword = "Admin@123";

//    var adminUser = await userManager.FindByEmailAsync(adminEmail);

//    if (adminUser == null)
//    {
//        adminUser = new ApplicationUser
//        {
//            UserName = adminEmail,
//            Email = adminEmail,
//            EmailConfirmed = true,
//            EmployeeNo="admin",
//            FullName="Admin"

//        };

//        var result = await userManager.CreateAsync(adminUser, adminPassword);

//        if (result.Succeeded)
//        {
//            await userManager.AddToRoleAsync(adminUser, "Admin");
//        }
//    }
//}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication(); 
app.UseAuthorization();

app.UseAntiforgery(); 

app.MapStaticAssets();
app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
      pattern: "{controller=Account}/{action=Login}/{id?}")
//pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
