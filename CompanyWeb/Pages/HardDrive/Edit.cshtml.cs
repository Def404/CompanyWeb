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

namespace CompanyWeb.Pages.HardDrive
{
    public class EditModel : PageModel
    {
        private readonly CompanyApi.Context.HardDriveCompanyContext _context;

        public EditModel(CompanyApi.Context.HardDriveCompanyContext context)
        {
            _context = context;
        }

        [BindProperty]
        public HardDriveP HardDriveP { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null || _context.HardDrives == null)
            {
                return NotFound();
            }

            var harddrivep =  await _context.HardDrives.FirstOrDefaultAsync(m => m.SerialNumber == id);
            if (harddrivep == null)
            {
                return NotFound();
            }
            HardDriveP = harddrivep;
           ViewData["ConnectionInterfaceId"] = new SelectList(_context.ConnectionInterfaceTypes, "ConnectionInterfaceId", "ConnectionInterfaceId");
           ViewData["DriveTypeId"] = new SelectList(_context.DriveTypes, "DriveTypeId", "DriveTypeId");
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

            _context.Attach(HardDriveP).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HardDrivePExists(HardDriveP.SerialNumber))
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

        private bool HardDrivePExists(long? id)
        {
          return (_context.HardDrives?.Any(e => e.SerialNumber == id)).GetValueOrDefault();
        }
    }
}
