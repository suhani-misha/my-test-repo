using SocialSecurity.Domain.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace SocialSecurity.Domain.Models
{
    public class Holiday : BaseModel
    {
        [Required(ErrorMessage = "Date is required.")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Holiday name is required.")]
        [StringLength(100, ErrorMessage = "Holiday name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        [StringLength(50, ErrorMessage = "Country name cannot exceed 50 characters.")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Holiday type is required.")]
        public string Type { get; set; }
    }
}
