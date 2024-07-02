using UsersProject.ApiSevices;
using UsersProject.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<DecryptionHandler>(); // Register DecryptionHandler

// Register the HttpClient with the decryption handler
builder.Services.AddHttpClient<WebApis>()
    .AddHttpMessageHandler<DecryptionHandler>(); // Add DecryptionHandler to HttpClient

builder.Services.AddRazorPages();
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7022/");
}).ConfigurePrimaryHttpMessageHandler(() =>
{
    return new HttpClientHandler
    {
        // If you need to handle cookies manually
        UseCookies = false
    };
});

builder.Services.AddScoped<WebApis>(); // Register WebApis

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
