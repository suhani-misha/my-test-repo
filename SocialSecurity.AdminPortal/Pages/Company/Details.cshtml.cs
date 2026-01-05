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
    public class DetailsModel : PageModel
    {
        private readonly SocialSecurity.Infrastructure.Data.AppDbContext _context;

        public DetailsModel(SocialSecurity.Infrastructure.Data.AppDbContext context)
        {
            _context = context;
        }

        public Company Company { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.Companies.FirstOrDefaultAsync(m => m.Id == id);

            if (company is not null)
            {
                Company = company;

                return Page();
            }

            return NotFound();
        }
    }
}
