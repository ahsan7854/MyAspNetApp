using Microsoft.AspNetCore.Mvc;

namespace MyApp.Web.Controllers;

public sealed class HomeController : Controller
{
    public IActionResult Index() => View();

    [Route("Home/Error")]
    public IActionResult Error() => View();
}
