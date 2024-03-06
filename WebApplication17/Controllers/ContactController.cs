using Microsoft.AspNetCore.Mvc;

namespace neurobalance.com.Controllers;
[ApiController]
[Route("[controller]")]
public class ContactController:Controller
{
    [HttpGet("/Contact")]
    public IActionResult Contacts()
    {
        
        return View();
    }
}