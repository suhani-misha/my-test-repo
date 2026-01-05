using SocialSecurity.Domain.Models;
using SocialSecurity.Domain.Models.Common;

namespace SocialSecurity.Domain.Models
{
    public class FiscalYear : BaseModel
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsCurrent { get; set; }
        // Navigation properties
        public List<FiscalYearPeriod> Periods { get; set; } = new List<FiscalYearPeriod>();
    }
}

public class FiscalYearPeriod : BaseModel
{
    public int FiscalYearId { get; set; }
    public string PeriodName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    // Navigation properties
    public FiscalYear FiscalYear { get; set; }
}
