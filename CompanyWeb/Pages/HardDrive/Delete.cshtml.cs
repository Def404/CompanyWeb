using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CompanyApi.Context;
using CompanyApi.Models.Posgres;

namespace CompanyWeb.Pages.HardDrive
{
    public class DeleteModel : PageModel
    {
        private readonly CompanyApi.Context.HardDriveCompanyContext _context;

        public DeleteModel(CompanyApi.Context.HardDriveCompanyContext context)
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

            var harddrivep = await _context.HardDrives.FirstOrDefaultAsync(m => m.SerialNumber == id);

            if (harddrivep == null)
            {
                return NotFound();
            }
            else 
            {
                HardDriveP = harddrivep;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null || _context.HardDrives == null)
            {
                return NotFound();
            }
            var harddrivep = await _context.HardDrives.FindAsync(id);

            if (harddrivep != null)
            {
                HardDriveP = harddrivep;
                _context.HardDrives.Remove(HardDriveP);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
