using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CompanyApi.Context;
using CompanyApi.Models.Posgres;

namespace CompanyWeb.Pages.HardDrive
{
    public class CreateModel : PageModel
    {
        private readonly CompanyApi.Context.HardDriveCompanyContext _context;

        public CreateModel(CompanyApi.Context.HardDriveCompanyContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ConnectionInterfaceId"] = new SelectList(_context.ConnectionInterfaceTypes, "ConnectionInterfaceId", "ConnectionInterfaceId");
        ViewData["DriveTypeId"] = new SelectList(_context.DriveTypes, "DriveTypeId", "DriveTypeId");
            return Page();
        }

        [BindProperty]
        public HardDriveP HardDriveP { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.HardDrives == null || HardDriveP == null)
            {
                return Page();
            }

            _context.HardDrives.Add(HardDriveP);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
