using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SocialSecurity.Domain.Models;
using SocialSecurity.Infrastructure.Data;

namespace SocialSecurity.AdminPortal.Pages_Student
{
    public class IndexModel : PageModel
    {
        private readonly SocialSecurity.Infrastructure.Data.AppDbContext _context;

        public IndexModel(SocialSecurity.Infrastructure.Data.AppDbContext context)
        {
            _context = context;
        }

        public IList<Student> Student { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Student = await _context.Students.ToListAsync();
        }
    }
}
