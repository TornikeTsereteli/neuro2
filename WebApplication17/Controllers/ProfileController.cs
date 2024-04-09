using Microsoft.AspNetCore.Mvc;

namespace neurobalance.com.Controllers;
[ApiController]
[Route("[controller]")]
public class ProfileController:Controller
{
    
    [HttpGet("/ProfilePage")]
    public IActionResult ProfilePage()
    {
        return View();
    }
    
    
    
}