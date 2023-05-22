using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CompanyApi.Context;
using CompanyApi.Models.Posgres;

namespace CompanyWeb.Pages.Employee
{
    public class IndexModel : PageModel
    {
        private readonly CompanyApi.Context.HardDriveCompanyContext _context;

        public IndexModel(CompanyApi.Context.HardDriveCompanyContext context)
        {
            _context = context;
        }

        public IList<CompanyApi.Models.Posgres.Employee> Employee { get;set; } = default!;

        public async Task OnGetAsync()
        {
            
            var currentLogin = HttpContext.User.Claims.FirstOrDefault().Value;
            
            var currentEmployee = _context.Employees.FirstOrDefault(e => e.EmployeeLogin == currentLogin);

            if (currentEmployee != null)
            {
                Employee = currentEmployee.PositionId switch
                {
                    3 => await _context.Employees
                        .Include(e => e.Position)
                        .ToListAsync(),
                    2 => await _context.Employees
                        .Where(e => e.PositionId != 3)
                        .Include(e => e.Position)
                        .ToListAsync(),
                    1 => await _context.Employees
                        .Where(e => e.EmployeeLogin == currentEmployee.EmployeeLogin)
                        .Include(e => e.Position)
                        .ToListAsync()
                };
            }
        }
    }
}
