using System.Security.Claims;
using CompanyApi;
using CompanyApi.Context;
using CompanyApi.Models;
using CompanyApi.Models.Posgres;
using CompanyApi.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CompanyWeb.Pages;
[AllowAnonymous]
public class Login : PageModel
{
    
    private readonly CompanyApi.Context.HardDriveCompanyContext _context;

    public Login(CompanyApi.Context.HardDriveCompanyContext context)
    {
        _context = context;
    }
    
    
    [BindProperty]
    public EmployeeLogin EmployeeLogin { get; set; }
    
    public void OnGet()
    {
        
    }
    
    public async Task<ActionResult> OnPost()
    {
        CompanyApi.Models.Posgres.Employee? employeeP;

        await using (HardDriveCompanyContext db = new HardDriveCompanyContext())
        {
            employeeP = db.Employees
                .FromSqlRaw("SELECT * FROM employee WHERE employee_login = {0} AND password = crypt({1}, password)",
                    EmployeeLogin.Login,
                    EmployeeLogin.Password)
                .FirstOrDefault();
        }

        if (employeeP == null)
            return Page();
        
        /*var employeesM = await EmployeeService.GetAsync();
        var employeeM = employeesM.FirstOrDefault(e => e.Login == EmployeeLogin.Login);*/

        /*if (employeeM == null)
            return Page();*/

        /*if (user.Position != "admin")
            return Page();*/

        var positionPosition = _context.EmployeesPositions.FirstOrDefault(w => w.PositionId == employeeP.PositionId);
                
        var claims = new[]{
            new Claim(ClaimTypes.NameIdentifier, EmployeeLogin.Login),
            new Claim(ClaimTypes.Role, positionPosition?.PositionName ?? "")
        };

        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        return RedirectToPage("/Index");
       
    }
}
