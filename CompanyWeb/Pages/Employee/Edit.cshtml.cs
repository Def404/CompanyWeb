using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CompanyApi.Context;
using CompanyApi.Models.Posgres;
using Microsoft.AspNetCore.Authorization;

namespace CompanyWeb.Pages.Employee
{
    [Authorize(Roles = "admin_c, manager")]
    public class EditModel : PageModel
    {
        private readonly CompanyApi.Context.HardDriveCompanyContext _context;

        public EditModel(CompanyApi.Context.HardDriveCompanyContext context)
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

            var employee =  await _context.Employees.FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }
            Employee = employee;
           ViewData["PositionId"] = new SelectList(_context.EmployeesPositions, "PositionId", "PositionId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(Employee.EmployeeId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool EmployeeExists(long id)
        {
          return (_context.Employees?.Any(e => e.EmployeeId == id)).GetValueOrDefault();
        }
    }
}
