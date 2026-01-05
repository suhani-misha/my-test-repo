using SocialSecurity.Domain.Models.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialSecurity.Domain.Models
{
    public class Function : BaseModel
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        public Guid DepartmentId { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public RiskRatingStatus RiskRating { get; set; }

        [Required]
        public LikelihoodStatus Likelihood { get; set; }

        [Required]
        public ImpactStatus Impact { get; set; }

        [Required]
        public ControlEffectivenessStatus Controls { get; set; }

        [MaxLength(200)]
        public string Responsible { get; set; }
        public string? Notes { get; set; }
    }
}
