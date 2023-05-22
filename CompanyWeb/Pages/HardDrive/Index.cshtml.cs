using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CompanyApi.Context;
using CompanyApi.Models.Mongo;
using CompanyApi.Models.Posgres;
using CompanyApi.Services;

namespace CompanyWeb.Pages.HardDrive
{
    public class IndexModel : PageModel
    {
        private readonly CompanyApi.Context.HardDriveCompanyContext _context;
        private readonly HardDriveService _hardDriveService;

        public IndexModel(CompanyApi.Context.HardDriveCompanyContext context, HardDriveService hardDriveService)
        {
            _context = context;
            _hardDriveService = hardDriveService;
        }

        public IList<HardDriveM> HardDriveM { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var t = await _hardDriveService.GetAsync();
            HardDriveM = t.Value.ToList();
        }
    }
}
