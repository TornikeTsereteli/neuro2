using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using neurobalance.com.Authorization;

namespace neurobalance.com.Models;


public class LoginViewModel: PageModel
{
    [BindProperty]
    public Credential Credential { get; set; } = new Credential();

    public LoginViewModel()
    {
        Credential = new Credential();
    }

    public void OnGet()
    {
        Console.WriteLine("get");
    }

    public async Task<IActionResult> OnPostAsync()
    {
        Console.WriteLine("post808098");
        if (!ModelState.IsValid) return Redirect("About/Team");

        // Verify the credential
        Console.WriteLine(Credential.UserName);
        Console.WriteLine(Credential.Password);
        if (Credential.UserName == "admin" && Credential.Password == "password") {
            Console.WriteLine(5555);
            // Creating the security context
            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, "admin"),
                new Claim(ClaimTypes.Email, "admin@mywebsite.com"),
                new Claim("Department", "HR"),
                new Claim("Admin", "true"),
                new Claim("Manager", "true"),
                new Claim("EmploymentDate", "2023-01-01")
            };
            var identity = new ClaimsIdentity(claims, "MyCookieAuth");
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = Credential.RememberMe
            };

            await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal, authProperties);

            return RedirectToPage("/Index");
        }

        return Redirect("/About/Services");

    }

}