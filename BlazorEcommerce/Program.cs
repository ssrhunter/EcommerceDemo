global using BlazorEcommerce.Shared;
global using BlazorEcommerce.Data;
global using Microsoft.EntityFrameworkCore;
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

app.UseSwaggerUI();

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

app.UseSwagger();

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BlazorEcommerce.Client._Imports).Assembly);

app.MapControllers();

app.Run();
