using SocialSecurity.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurity.Shared.Dtos.FiscalYear
{
    public class FiscalYearDto
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsCurrent { get; set; }
        public List<string> PeriodNames { get; set; } // List of period names like "Quarter 1", "Quarter 2", etc.
    }
}
