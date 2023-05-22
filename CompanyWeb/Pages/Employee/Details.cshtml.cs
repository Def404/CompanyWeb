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
    public class DetailsModel : PageModel
    {
        private readonly CompanyApi.Context.HardDriveCompanyContext _context;

        public DetailsModel(CompanyApi.Context.HardDriveCompanyContext context)
        {
            _context = context;
        }

      public CompanyApi.Models.Posgres.Employee Employee { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }
            else 
            {
                Employee = employee;
            }
            return Page();
        }
    }
}
