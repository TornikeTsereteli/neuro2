using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace neurobalance.com.Controllers;
[ApiController]
[Route("[controller]")]
public class NewsController:Controller
{
    [HttpGet("/News/Rubric")]
    public IActionResult Rubric()
    {
        return View();
    }
    [HttpGet("/News/Activities")]
    public IActionResult Activities()
    {
        return View();
    }
     
    
}