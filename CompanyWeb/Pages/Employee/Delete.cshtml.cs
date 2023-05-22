using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CompanyApi.Context;
using CompanyApi.Models.Posgres;
using Microsoft.AspNetCore.Authorization;

namespace CompanyWeb.Pages.Employee
{
    [Authorize(Roles = "admin_c, manager")]
    public class DeleteModel : PageModel
    {
        private readonly CompanyApi.Context.HardDriveCompanyContext _context;

        public DeleteModel(CompanyApi.Context.HardDriveCompanyContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }
            var employee = await _context.Employees.FindAsync(id);

            if (employee != null)
            {
                Employee = employee;
                _context.Employees.Remove(Employee);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
