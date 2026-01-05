using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SocialSecurity.Domain.Models;
using SocialSecurity.Infrastructure.Data;

namespace SocialSecurity.AdminPortal.Pages_Company
{
    public class IndexModel : PageModel
    {
        private readonly SocialSecurity.Infrastructure.Data.AppDbContext _context;

        public IndexModel(SocialSecurity.Infrastructure.Data.AppDbContext context)
        {
            _context = context;
        }

        public IList<Company> Company { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Company = await _context.Companies.ToListAsync();
        }
    }
}
