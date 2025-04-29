using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVC_EF_Start_8.Models;
using MVC_EF_Start_8.Services;
using System.Net.Http;
using Newtonsoft.Json;
using System.Linq;

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
                        _logger.LogInformation("Added {0} outages to the service.", apiData.response.data.Count);
                    }
                    else
                    {
                        _logger.LogWarning("API returned null or empty data.");
                    }
                }
                else
                {
                    _logger.LogWarning("API request failed: {0}", response.StatusCode);
                }
            }

            var outagesList = await _nuclearOutageService.GetAllOutagesAsync();
            _logger.LogInformation("Outages List Count after fetch: {0}", outagesList.Count);
            return View(outagesList);
        }

        public async Task<IActionResult> DataVisualization()
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
                        _logger.LogInformation("Added {0} outages to the service (DataVisualization).", apiData.response.data.Count);
                    }
                    else
                    {
                        _logger.LogWarning("API returned null or empty data (DataVisualization).");
                    }
                }
                else
                {
                    _logger.LogWarning("API request failed (DataVisualization): {0}", response.StatusCode);
                }
            }

            var outagesList = await _nuclearOutageService.GetAllOutagesAsync();
            _logger.LogInformation("Outages List Count in DataVisualization: {0}", outagesList.Count);
            return View(outagesList);
        }

        public async Task<IActionResult> GetChartData()
        {
            var outagesList = await _nuclearOutageService.GetAllOutagesAsync();
            _logger.LogInformation("Fetched Outages List Count for Chart: {0}", outagesList.Count);

            var facilityRegionMap = FacilityRegionMap.Regions;

            var dailyOutageMap = new Dictionary<string, int>();
            var generatorOutageMap = new Dictionary<string, int>();
            var generatorFrequencyMap = new Dictionary<string, int>();

            foreach (var outage in outagesList)
            {
                _logger.LogInformation("period={0}, facilityName={1}, outage={2}", outage.period, outage.facilityName, outage.outage);

                string period = outage.period;

                if (double.TryParse(outage.outage, out double outageVal))
                {
                    int outageValue = (int)Math.Round(outageVal);

                    if (outageValue > 0 && !string.IsNullOrWhiteSpace(outage.facilityName))
                    {
                        if (!dailyOutageMap.ContainsKey(period))
                            dailyOutageMap[period] = 0;
                        dailyOutageMap[period] += outageValue;

                        string rawName = outage.facilityName.Trim();
                        string region = "Unknown";

                        if (facilityRegionMap.TryGetValue(rawName, out var exactRegion))
                        {
                            region = exactRegion;
                        }
                        else
                        {
                            var fallbackMatch = facilityRegionMap.FirstOrDefault(kvp => rawName.Contains(kvp.Key));
                            if (!string.IsNullOrWhiteSpace(fallbackMatch.Key))
                            {
                                region = fallbackMatch.Value;
                            }
                        }

                        string label = $"{rawName} ({region})";

                        if (!generatorOutageMap.ContainsKey(label))
                            generatorOutageMap[label] = 0;
                        generatorOutageMap[label] += outageValue;

                        if (!generatorFrequencyMap.ContainsKey(label))
                            generatorFrequencyMap[label] = 0;
                        generatorFrequencyMap[label]++;
                    }
                }
            }

            var sortedDailyOutages = dailyOutageMap.OrderBy(kv => kv.Key).ToList();
            var sortedTopGeneratorOutages = generatorOutageMap
                .OrderByDescending(kv => kv.Value)
                .Take(10)
                .ToList();
            var sortedTopGeneratorFrequencies = generatorFrequencyMap
                .OrderByDescending(kv => kv.Value)
                .Take(10)
                .ToList();

            var response = new
            {
                dailyOutages = new
                {
                    labels = sortedDailyOutages.Select(kv => kv.Key).ToList(),
                    values = sortedDailyOutages.Select(kv => kv.Value).ToList()
                },
                generatorOutages = new
                {
                    labels = sortedTopGeneratorOutages.Select(kv => kv.Key).ToList(),
                    values = sortedTopGeneratorOutages.Select(kv => kv.Value).ToList()
                },
                generatorFrequency = new
                {
                    labels = sortedTopGeneratorFrequencies.Select(kv => kv.Key).ToList(),
                    values = sortedTopGeneratorFrequencies.Select(kv => kv.Value).ToList()
                }
            };

            _logger.LogInformation("Returning chart data JSON: {0}", JsonConvert.SerializeObject(response));
            return Json(response);
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