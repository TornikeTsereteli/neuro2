using Microsoft.AspNetCore.Mvc;

namespace neurobalance.com.Controllers;
[ApiController]
[Route("[controller]")]
public class FondController: Controller
{
    [HttpGet("/Fond/Donors")]
    public IActionResult Donors()
    {
        return View();
    }
    [HttpGet("/Fond/Transparency")]
    public IActionResult Transparency()
    {
        return View();
    }
    [HttpGet("/Fond/Philanthropy")]
    public IActionResult Philanthropy()
    {
        return View();
    }
}