using Dreamers.Ui.Infrastructure;
using Dreamers.Ui.Models;
using Dreamers.Ui.Repositories;
using ElasticEmail.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using X.Paymob.CashIn;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages().AddRazorPagesOptions(options =>
{
    options.Conventions.AddPageRoute("/admin/excursions/list", "admin/excursions");
    //options.Conventions.AddPageRoute("/admin/excursions/insert", "admin/excursions/insert");
    options.Conventions.AddPageRoute("/admin/excursions/edit", "admin/excursions/edit/{excursionId}");

    options.Conventions.AddPageRoute("/excursion-details", "excursions/{excursionUrlName}");
    options.Conventions.AddPageRoute("/receipt", "receipt/{bookingKey}");
});

builder.Services.AddScoped<ExcursionRepo>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IRazorPartialToStringRenderer, RazorPartialToStringRenderer>();

builder.Services.Configure<PaymobConfiguration>(builder.Configuration.GetSection("PaymobConfiguration"));
builder.Services.AddPaymobCashIn(config =>
{
    config.ApiKey = builder.Configuration.GetValue<string>("PaymobConfiguration:ApiKey");
    config.Hmac = builder.Configuration.GetValue<string>("PaymobConfiguration:Hmac");
});

// Add configuration
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();
var connectionString = configuration.GetConnectionString("DefaultConnection");
// Register your DbContext with dependency injection
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
