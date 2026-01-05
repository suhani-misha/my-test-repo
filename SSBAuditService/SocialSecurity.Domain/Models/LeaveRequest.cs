using SocialSecurity.Domain.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace SocialSecurity.Domain.Models
{
    public class LeaveRequest : BaseModel
    {
        public string? RequestId {get; set; }
        [Required(ErrorMessage = "Start date is required.")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "End date is required.")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "User id is required.")]
        public Guid UserId { get; set; }
        [StringLength(250, ErrorMessage = "Reason cannot exceed 250 characters.")]
        public  string Reason { get; set; }
        public int Duaration { get; set; }
        [StringLength(250, ErrorMessage = "Reason cannot exceed 250 characters.")]
        public string Status { get; set; }
    }
}
