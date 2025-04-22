using Group4_Web_App.Models;
using Group4_Web_App.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace DATAGOV_API_INTRO_8.Controllers
{
    public class HomeController : Controller
    {
        private readonly ParksService _parksService;
        private readonly HttpClient _httpClient;

        // API Base URL and API Key
        static string BASE_URL = "https://api.eia.gov/v2/";
        static string API_KEY = "10Cb31KivaDpOJGrdIbAq8gUsF2Mq0kNMWQQzygT";

        public HomeController(ParksService parksService)
        {
            _parksService = parksService;
            _httpClient = new HttpClient();
        }

        // READ: Index action to display all parks
        public async Task<IActionResult> Index()
        {
            Console.WriteLine("Index action called.");

            // If the data hasn't been fetched yet, fetch data from the API
            if (!_parksService.IsDataFetched())
            {
                Console.WriteLine("Fetching data from API...");
                string apiPath = $"{BASE_URL}nuclear-outages/generator-nuclear-outages/data/?api_key={API_KEY}";

                HttpResponseMessage response = await _httpClient.GetAsync(apiPath);
                Console.WriteLine($"API response status code: {response.StatusCode}");

                if (response.IsSuccessStatusCode)
                {
                    string parksData = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Data fetched from API successfully.");
                    Console.WriteLine($"JSON Response: {parksData}");

                    // Deserialize the JSON data
                    // https://json2csharp.com/
                    Parks parks = JsonConvert.DeserializeObject<Parks>(parksData);

                    if (parks != null && parks.data != null)
                    {
                        Console.WriteLine($"Number of parks fetched: {parks.data.Count}");
                        _parksService.AddParks(parks.data);  // Store API data in memory
                        _parksService.MarkDataAsFetched();   // Mark data as fetched
                    }
                    else
                    {
                        Console.WriteLine("Deserialization failed or no data available.");
                    }
                }
                else
                {
                    Console.WriteLine("Failed to fetch data from API.");
                }
            }
            else
            {
                Console.WriteLine("Data already fetched.");
            }

            // Retrieve all parks from the service
            var parksList = _parksService.GetAllParks();
            Console.WriteLine($"Number of parks in service: {parksList.Count}");

            // Return the list of parks to the view
            return View(parksList);
        }

        // CREATE: Handle both GET (display form) and POST (create park)
        [HttpGet, HttpPost]
        public IActionResult Create(Park newPark)
        {
            Console.WriteLine("Create action called.");

            if (HttpContext.Request.Method == "POST")
            {
                _parksService.AddPark(newPark);  // Add the new park to the service
                Console.WriteLine($"New park created: {newPark.fullName}");
                return RedirectToAction("Index");  // Redirect to Index after successful creation
            }

            return View();
        }

        // UPDATE: Handle both GET (show edit form) and POST (edit park)
        [HttpGet, HttpPost]
        public IActionResult Edit(string id, Park updatedPark)
        {
            Console.WriteLine($"Edit action called for Park ID: {id}");

            if (HttpContext.Request.Method == "POST")
            {
                if (ModelState.IsValid) // Basic validation check
                {
                    bool success = _parksService.UpdatePark(id, updatedPark);
                    if (success)
                    {
                        Console.WriteLine($"Park updated successfully: {updatedPark.fullName}");
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        Console.WriteLine("Park update failed. Park not found.");
                        return NotFound();
                    }
                }

                Console.WriteLine("Model validation failed. Returning to Edit view.");
                return View(updatedPark); // Return view with validation messages if invalid
            }

            var park = _parksService.GetParkById(id);
            if (park == null)
            {
                Console.WriteLine("Park not found for editing. Returning NotFound.");
                return NotFound();
            }

            return View(park);
        }

        // DELETE: Handle both GET (display confirmation) and POST (delete park)
        [HttpGet, HttpPost]
        public IActionResult Delete(string id)
        {
            Console.WriteLine($"Delete action called for Park ID: {id}");

            if (HttpContext.Request.Method == "POST")
            {
                bool success = _parksService.DeletePark(id);
                if (success)
                {
                    Console.WriteLine($"Park deleted successfully: ID = {id}");
                    return RedirectToAction("Index");
                }
                else
                {
                    Console.WriteLine("Park deletion failed. Park not found.");
                    return NotFound();
                }
            }

            var park = _parksService.GetParkById(id);
            if (park == null)
            {
                Console.WriteLine("Park not found for deletion. Returning NotFound.");
                return NotFound();
            }

            return View(park);
        }

        // Privacy action for Privacy page
        public IActionResult Privacy()
        {
            Console.WriteLine("Privacy page accessed.");
            return View();
        }
    }
}
