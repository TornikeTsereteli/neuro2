using Microsoft.AspNetCore.Mvc;
using neurobalance.com.Models;

namespace neurobalance.com.Controllers;

public class UserController : Controller
{
    public IActionResult Hello()
    {
        // Retrieve data from a database or some other source
        var user = new User { Id = 1, Name = "John Doe", Email = "john@example.com" };
            
        return View(user); // Pass the user object to the view
    }
}