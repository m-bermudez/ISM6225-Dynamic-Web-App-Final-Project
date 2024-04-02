using Microsoft.AspNetCore.Mvc;

namespace MVC_EF_Start_8.Controllers
{
  public class HomeController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }
  }
}