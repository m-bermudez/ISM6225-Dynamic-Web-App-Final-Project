using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVC_EF_Start_8.Models;
using MVC_EF_Start_8.Services;
using System.Net.Http;
using Newtonsoft.Json;

namespace MVC_EF_Start_8.Controllers
{
    public class HomeController : Controller
    {
        private const string BASE_URL = "https://api.eia.gov/v2/";
        private const string API_KEY = "10Cb31KivaDpOJGrdIbAq8gUsF2Mq0kNMWQQzygT";
        private readonly NuclearOutageService _nuclearOutageService;
        private readonly HttpClient _httpClient;
        private readonly ILogger<HomeController> _logger;

        private readonly string apiPath = $"{BASE_URL}nuclear-outages/generator-nuclear-outages/data/" +
            $"?frequency=daily" +
            $"&data[0]=capacity&data[1]=outage&data[2]=percentOutage" +
            $"&sort[0][column]=period&sort[0][direction]=desc" +
            $"&offset=0&length=5000" +
            $"&api_key={API_KEY}";

        public HomeController(NuclearOutageService nuclearOutageService, ILogger<HomeController> logger)
        {
            _nuclearOutageService = nuclearOutageService;
            _logger = logger;
            _httpClient = new HttpClient();
        }

        public async Task<IActionResult> Index()
        {
            if (!_nuclearOutageService.IsDataFetched())
            {
                _logger.LogInformation("Fetching data from API...");
                HttpResponseMessage response = await _httpClient.GetAsync(apiPath);

                if (response.IsSuccessStatusCode)
                {
                    string outageData = await response.Content.ReadAsStringAsync();
                    Root apiData = JsonConvert.DeserializeObject<Root>(outageData);

                    if (apiData?.response?.data != null)
                    {
                        await _nuclearOutageService.AddOutagesAsync(apiData.response.data);
                        _nuclearOutageService.MarkDataAsFetched();
                    }
                }
            }

            var outagesList = await _nuclearOutageService.GetAllOutagesAsync();
            return View(outagesList);
        }

        public async Task<IActionResult> DataVisualization()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(apiPath);
                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = await response.Content.ReadAsStringAsync();
                    Root apiData = JsonConvert.DeserializeObject<Root>(jsonResult);

                    return View(apiData?.response?.data ?? new List<OutageRecord>());
                }
                ViewBag.ApiError = $"API request failed: {response.StatusCode}";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception during API call");
                ViewBag.ApiError = ex.Message;
            }
            return View(new List<OutageRecord>());
        }

        public IActionResult About() => View();
        public IActionResult Create() => View();
        public IActionResult Update() => View();
        public IActionResult Delete() => View();

        public async Task<IActionResult> Read()
        {
            var outages = await _nuclearOutageService.GetAllOutagesAsync();
            return View(outages);
        }
    }
}