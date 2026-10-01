
// Import the namespace for the application.
using CIE_2;

// Create the application builder.
var builder = WebApplication.CreateBuilder(args);

// Register MVC controllers and Razor views.
builder.Services.AddControllersWithViews();

// Register in-memory caching for the doctor list.
builder.Services.AddMemoryCache();

// Register HttpClient for asynchronous API requests.
builder.Services.AddHttpClient();

// Register response caching services.
builder.Services.AddResponseCaching();

// Register session services and configure session settings.
builder.Services.AddSession(options =>
{
    // Set session expiration after 20 minutes of inactivity.
    options.IdleTimeout = TimeSpan.FromMinutes(20);

    // Prevent JavaScript from accessing the session cookie.
    options.Cookie.HttpOnly = true;

    // Mark the session cookie as essential.
    options.Cookie.IsEssential = true;
});

// Build the application.
var app = builder.Build();

// Enable serving static files such as CSS and JavaScript.
app.UseStaticFiles();

// Enable routing to identify the requested endpoint.
app.UseRouting();

// Enable session storage for the application.
app.UseSession();

// Enable response caching middleware after routing.
app.UseResponseCaching();

// Enable authorization middleware.
app.UseAuthorization();

// Configure the default MVC route.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Doctor}/{action=Index}/{id?}");

// Start the application.
app.Run();