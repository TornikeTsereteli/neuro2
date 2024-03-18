using Microsoft.AspNetCore.Mvc;

namespace neurobalance.com.Controllers;
[ApiController]
[Route("[controller]")]
public class AboutController:Controller
{

    [HttpGet("/About")]
    public IActionResult About()
    {
        return View();
    }
    
    [HttpGet("/About/Services")]
    public IActionResult Services()
    {
        return View();
    }

    [HttpGet("/About/Team")]
    public IActionResult Team()
    {
        return View();
    }
    
    
}