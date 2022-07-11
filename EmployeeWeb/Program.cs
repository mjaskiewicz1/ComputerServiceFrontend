using Common.Interfaces;
using Common.Repositories;
using Common.Services;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var provider = builder.Services.BuildServiceProvider();
var configuration = provider.GetRequiredService<IConfiguration>();



builder.Services.AddScoped<ITemplateService, RazorViewsTemplateService>();
builder.Services.AddScoped<IAzureBlobService, AzureBlobService>();
builder.Services.AddScoped<IEmailSenderService, EmailSenderService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddHttpClient<IClientWebApi, ClientWebApi>();
builder.Services.AddScoped<IConfigService, ConfigService>();
builder.Services.AddScoped<IConfigApiRepository, ConfigApiRepository>();
builder.Services.AddScoped<IProductApiRepository, ProductApiRepository>();
builder.Services.AddScoped<IRequestApiRepository, RequestApiRepository>();
builder.Services.AddScoped<IInvoiceApiRepository, InvoiceApiRepository>();
builder.Services.AddScoped<IServiceApiRepository, ServiceApiRepository>();
builder.Services.AddScoped<IRequestService, RequestService>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IServiceService, ServiceService>();

builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(configuration.GetSection("AzureAd"))
    .EnableTokenAcquisitionToCallDownstreamApi(new[]
    {
        "User.Read", "User.ReadBasic.All"
    })
    .AddInMemoryTokenCaches().AddMicrosoftGraph();

builder.Services.AddControllersWithViews(options =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    options.Filters.Add(new AuthorizeFilter(policy));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseRequestLocalization("pl-PL");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Index}/{id?}");

app.Run();