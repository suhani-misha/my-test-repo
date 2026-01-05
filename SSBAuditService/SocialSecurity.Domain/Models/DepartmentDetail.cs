using SocialSecurity.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurity.Domain.Models
{
    public class DepartmentDetail : BaseModel
    {
        public Guid DepartmentId { get; set; }
        public string Email { get; set; }
        public string DepartmentHead { get; set; }
        public string Location { get; set; }
        public string Phone { get; set; }
        public string RiskRating { get; set; }
        //navigation property
        public ICollection<DepartmentFunction> DepartmentFunctions { get; set; } = new List<DepartmentFunction>();
    }

    public class DepartmentFunction : BaseModel
    {
        public Guid DepartmentId { get; set; }
        public int DepartmentDetailsId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string RiskRating { get; set; }
        public string Likehood { get; set; }
        public string Impact { get; set; }
        public string Controls { get; set; }
        public string Responsible { get; set; }
        //navigation property
        public DepartmentDetail DepartmentDetails { get; set; }
    }
}
