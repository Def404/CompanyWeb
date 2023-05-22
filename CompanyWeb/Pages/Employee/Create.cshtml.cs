using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CompanyApi.Context;
using CompanyApi.Models.Posgres;
using Microsoft.AspNetCore.Authorization;

namespace CompanyWeb.Pages.Employee
{
    [Authorize(Roles = "admin_c, manager")]
    public class CreateModel : PageModel
    {
        private readonly CompanyApi.Context.HardDriveCompanyContext _context;

        public CreateModel(CompanyApi.Context.HardDriveCompanyContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["PositionId"] = new SelectList(_context.EmployeesPositions, "PositionId", "PositionId");
            return Page();
        }

        [BindProperty]
        public CompanyApi.Models.Posgres.Employee Employee { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Employees == null || Employee == null)
            {
                return Page();
            }

            _context.Employees.Add(Employee);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
