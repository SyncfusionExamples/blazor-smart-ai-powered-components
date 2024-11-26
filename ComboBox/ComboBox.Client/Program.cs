using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Syncfusion.Blazor;
using SmartComponents.LocalEmbeddings;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddSyncfusionBlazor();
builder.Services.AddSingleton<LocalEmbedder>();

await builder.Build().RunAsync();
