using BookingManagement.Components;
using BookingManagement.Database;
using BookingManagement.Extensions;
using BookingManagement.Interfaces;
using BookingManagement.Services;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<BookingManagementDbContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("BookingConnection")));

builder.Services.AddScoped<ITicketService, TicketService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    //app.ApplyMigrations();  
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
