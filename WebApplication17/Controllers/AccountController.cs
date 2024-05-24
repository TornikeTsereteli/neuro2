using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using neurobalance.com.Authorization;
using neurobalance.com.Models;

namespace neurobalance.com.Controllers;

public class AccountController:Controller
{

    private readonly LoginViewModel _loginViewModel;
    
    public AccountController(LoginViewModel loginViewModel)
    {
        _loginViewModel = loginViewModel;
    }

    [HttpGet("Account/Login")]
    public IActionResult Login()
    {
       
        return View(_loginViewModel);
    }

    [HttpPost("Account/SignIn")]
    public async Task<IActionResult> SignIn()
    {
        return await _loginViewModel.OnPostAsync();
    }
    
}