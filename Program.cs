using UsersProject.ApiSevices;
using UsersProject.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Register the encryption handler
builder.Services.AddTransient<EncryptionHandler>();
// Register the HttpClient with the encryption handler
builder.Services.AddHttpClient<WebApis>()
    .AddHttpMessageHandler<EncryptionHandler>();
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

// Register HttpClient
builder.Services.AddScoped<WebApis>();
builder.Services.AddHttpClient();
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