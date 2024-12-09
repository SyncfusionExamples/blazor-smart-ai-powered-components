using Syncfusion.Blazor.SmartComponents;
using Syncfusion.Blazor;
using TreeGrid.Components;
using TreeGrid.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSyncfusionBlazor();

string apiKey = "apiKey";
string deploymentName = "deploymentName";
string endpoint = "endpoint";

builder.Services.AddSyncfusionSmartComponents()
.ConfigureCredentials(new AIServiceCredentials(apiKey, deploymentName, endpoint))
.InjectOpenAIInference();

builder.Services.AddSingleton<OpenAIConfiguration>();
builder.Services.AddSingleton<AzureAIService>();


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
