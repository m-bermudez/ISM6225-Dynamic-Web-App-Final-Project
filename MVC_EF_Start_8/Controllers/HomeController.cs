using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_EF_Start_8.DataAccess;
using MVC_EF_Start_8.Models;


namespace MVC_EF_Start_8.Controllers
{
  public class HomeController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }

    public IActionResult About()
    {
        return View();
    }

    // In your Controller (e.g., HomeController.cs)
    public IActionResult DataVisualization()
    {
        var rand = new Random();

        // Generate synthetic data
        var model = new ChartDataViewModel
        {
            Labels = new List<string> { "Jan", "Feb", "Mar", "Apr", "May", "Jun" },
            Values = Enumerable.Range(0, 6).Select(_ => rand.Next(100, 1000)).ToList(),
            DataSetLabel = "Synthetic Sales Data",
            MultiDatasetValues = new List<Dataset>
            {
                new Dataset {
                    Label = "Product A",
                    Data = Enumerable.Range(0, 6).Select(_ => rand.Next(100, 500)).ToList(),
                    BackgroundColor = "#3e95cd"
                },
                new Dataset {
                    Label = "Product B",
                    Data = Enumerable.Range(0, 6).Select(_ => rand.Next(100, 500)).ToList(),
                    BackgroundColor = "#8e5ea2"
                }
            }
        };
        return View(model);
    }

    public IActionResult Create()
    {
        return View();
    }

    public IActionResult Read()
    {
        return View();
    }

    public IActionResult Update()
    {
        return View();
    }

    public IActionResult Delete()
    {
        return View();
    }

  }
}