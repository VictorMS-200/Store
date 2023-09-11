using Microsoft.EntityFrameworkCore;
using Store.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddDbContext<StoreWebContext>(opts =>
    opts.UseSqlite(builder.Configuration.GetConnectionString("StoreWebContext")));

var app = builder.Build();

app.UseDeveloperExceptionPage();
app.UseStaticFiles();
app.UseRouting();
app.MapDefaultControllerRoute();

app.Run();
