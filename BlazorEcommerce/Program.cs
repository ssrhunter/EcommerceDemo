global using BlazorEcommerce.Shared;
global using BlazorEcommerce.Data;
global using Microsoft.EntityFrameworkCore;
global using BlazorEcommerce.Services.ProductService;
global using BlazorEcommerce.Services.CategoryService;
global using BlazorEcommerce.Client.Services.ProductService;
// Remember to add the client size services on the server because of the initial server side render.
global using BlazorEcommerce.Client.Services.CategoryService;
global using BlazorEcommerce.Client.Components;
using BlazorEcommerce.Client.Pages;
using BlazorEcommerce.Components;

var builder = WebApplication.CreateBuilder(args);

// Added by Ross, 2025-09-29
builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Passing in the custom DataContext from BlazorEcommerce.Data
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<BlazorEcommerce.Services.ProductService.IProductService, BlazorEcommerce.Services.ProductService.ProductService>();
builder.Services.AddScoped<BlazorEcommerce.Client.Services.ProductService.IProductService, BlazorEcommerce.Client.Services.ProductService.ProductService>();
builder.Services.AddScoped<BlazorEcommerce.Services.CategoryService.ICategoryService, BlazorEcommerce.Services.CategoryService.CategoryService>();
builder.Services.AddScoped<BlazorEcommerce.Client.Services.CategoryService.ICategoryService, BlazorEcommerce.Client.Services.CategoryService.CategoryService>();


// SQL Connection String:
// Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;
// SQL Log Folder:
// C:\Program Files\Microsoft SQL Server\160\Setup Bootstrap\Log\20250929_192001

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddHttpClient();  
//client =>
//{
//    // Add the default base URI so we don't have to keep specifiying it.
//    // TODO: Could move this to a config file later.
//    client.BaseAddress = new Uri("/", UriKind.Relative);
//});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();

// Really strange, but I had to add this to stop the
// favicon from being routed to the /api routes.
// UseRouting() needs to come after UseStaticFiles().
app.UseRouting();

app.UseSwaggerUI();

app.UseSwagger();

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BlazorEcommerce.Client._Imports).Assembly);

app.MapControllers();

app.Run();
