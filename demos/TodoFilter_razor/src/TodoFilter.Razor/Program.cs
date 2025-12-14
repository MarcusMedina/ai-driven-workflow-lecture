using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TodoFilter.Razor;
using TodoFilter.Razor.Data;
using TodoFilter.Razor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton<TodoRepository>();
builder.Services.AddScoped<TodoFilterService>();

await builder.Build().RunAsync();
