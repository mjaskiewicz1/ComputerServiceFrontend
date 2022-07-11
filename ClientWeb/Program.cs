using Common.Helppers;
using Common.Interfaces;
using Common.Repositories;
using Common.Services;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);
var provider = builder.Services.BuildServiceProvider();
// Add services to the container.
builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});
var Configuration = provider.GetRequiredService<IConfiguration>();

// Configure the HTTP request pipeline.


builder.Services.AddHttpClient<IClientWebApi, ClientWebApi>();
builder.Services.AddScoped<IConfigService, ConfigService>();
builder.Services.AddScoped<IConfigApiRepository, ConfigApiRepository>();
builder.Services.AddScoped<IRequestApiRepository, RequestApiRepository>();
builder.Services.AddScoped<IRequestService, RequestService>();
builder.Services.AddScoped<IAzureBlobService, AzureBlobService>();
builder.Services.AddScoped<IEmailSenderService, EmailSenderService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

var app = builder.Build();
if (!app.Environment.IsDevelopment()) app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseRequestLocalization("pl-PL");
app.UseAuthorization();

app.MapControllerRoute(
    "default",
    "{controller=Request}/{action=Create}/{id?}");

app.Run();