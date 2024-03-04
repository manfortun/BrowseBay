using BrowseBay.DataAccess;
using BrowseBay.Service;
using BrowseBay.Service.Services;
using BrowseBay.Service.Services.Interfaces;
using BrowseBay.Service.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireDigit = false;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddAuthorization(options =>
{
    foreach (Policy policyEnum in Enum.GetValues(typeof(Policy)))
    {
        string[]? roles = policyEnum.GetAttribute<RolesAttribute>()?.Roles;

        if (roles != null && roles.Any())
        {
            options.AddPolicy(policyEnum.ToString(), policy =>
            {
                policy.RequireRole(roles);
            });
        }
    }
});

builder.Services.AddTransient<UserValidator<IdentityUser>>();
builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<IUploadService<IFormFile>, FormFileUploadService>();
builder.Services.AddMvc(options =>
{
    var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

    options.Filters.Add(new AuthorizeFilter(policy));
}).AddXmlSerializerFormatters();

builder.Services.ConfigureApplicationCookie(config =>
{
    config.LoginPath = new PathString("/account/login");
    config.AccessDeniedPath = new PathString("/account/login");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=account}/{action=login}/{id?}");

app.Run();
