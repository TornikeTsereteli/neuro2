using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace neurobalance.com.Models;
[Authorize("tokenonly")]
public class ProfileViewModel:PageModel
{
    
    
     
}