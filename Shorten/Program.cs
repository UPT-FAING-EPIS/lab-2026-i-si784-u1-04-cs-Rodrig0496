using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shorten.Areas.Identity.Data;
using Shorten.Areas.Domain;
using Shorten.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ShortenIdentityDbContextConnection") ?? throw new InvalidOperationException("Connection string not found.");

builder.Services.AddDbContext<ShortenIdentityDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ShortenIdentityDbContext>();
builder.Services.AddDbContext<ShortenContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddQuickGridEntityFrameworkAdapter();
builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.Run();
