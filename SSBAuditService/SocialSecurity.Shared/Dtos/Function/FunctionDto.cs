using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurity.Shared.Dtos.Function
{
    public class FunctionDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string Description { get; set; }
        public string RiskRating { get; set; }
        public string Likelihood { get; set; }
        public string Impact { get; set; }
        public string Controls { get; set; }
        public string Responsible { get; set; }
        public string Notes { get; set; }
    }

    public class FunctionCreateDto
    {
        public string Name { get; set; }
        public long DepartmentId { get; set; }
        public string Description { get; set; }
        public string RiskRating { get; set; }
        public string Likelihood { get; set; }
        public string Impact { get; set; }
        public string Controls { get; set; }
        public string Responsible { get; set; }
        public string Notes { get; set; }
    }

    public class FunctionStatsDto
    {
        public int TotalFunctions { get; set; }
        public int HighRiskCount { get; set; }
        public int MediumRiskCount { get; set; }
        public int LowRiskCount { get; set; }
    }

}
