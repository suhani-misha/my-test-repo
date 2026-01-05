
namespace SocialSecurity.Domain.Models.Common
{
    public class BaseModel
    {
        public int Id { get; set; } //Internel PK
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public int CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;
        public int ModifiedBy { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
    }
}
