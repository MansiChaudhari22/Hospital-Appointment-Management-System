
// Import the MVC namespace.
using Microsoft.AspNetCore.Mvc;

// Import JSON deserialization functionality.
using System.Text.Json;

// Import the Post model.
using CIE_2.Models;

// Define the controller namespace.
namespace CIE_2.Controllers
{
    // Handle home page and external API requests.
    public class HomeController : Controller
    {
        // Store the injected HttpClient.
        private readonly HttpClient _httpClient;

        // Inject HttpClient through the constructor.
        public HomeController(HttpClient httpClient)
        {
            // Assign the injected client to the field.
            _httpClient = httpClient;
        }

        // Display the home page.
        public IActionResult Index()
        {
            // Return the home view.
            return View();
        }

        // Display the privacy page.
        public IActionResult Privacy()
        {
            // Return the privacy view.
            return View();
        }

        // Fetch posts asynchronously from the external API.
        public async Task<IActionResult> Posts()
        {
            // Store the JSONPlaceholder API URL.
            string apiUrl = "https://jsonplaceholder.typicode.com/posts";

            try
            {
                // Send an asynchronous GET request.
                var response = await _httpClient.GetAsync(apiUrl);

                // Throw an exception if the response is unsuccessful.
                response.EnsureSuccessStatusCode();

                // Read the response content asynchronously.
                var json = await response.Content.ReadAsStringAsync();

                // Convert the JSON response into Post objects.
                var posts = JsonSerializer.Deserialize<List<Post>>(
                    json,
                    new JsonSerializerOptions
                    {
                        // Ignore differences in property-name capitalization.
                        PropertyNameCaseInsensitive = true
                    });

                // Pass the retrieved posts to the view.
                return View(posts ?? new List<Post>());
            }
            catch (HttpRequestException)
            {
                // Store an error message if the API request fails.
                ViewBag.Error = "Unable to retrieve posts. Please try again.";

                // Return the view with an empty list.
                return View(new List<Post>());
            }
            catch (JsonException)
            {
                // Store an error message if JSON parsing fails.
                ViewBag.Error = "The API returned invalid data.";

                // Return the view with an empty list.
                return View(new List<Post>());
            }
        }
    }
}