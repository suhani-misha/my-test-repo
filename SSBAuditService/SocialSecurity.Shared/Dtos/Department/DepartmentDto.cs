namespace SocialSecurity.Shared.Dtos.Department
{
    public class DepartmentDetailsDto
    {
        public int Id { get; set; }
        public Guid DepartmentId { get; set; }
        public string Email { get; set; }
        public string DepartmentHead { get; set; }
        public string Location { get; set; }
        public string Phone { get; set; }
        public string RiskRating { get; set; }

        // Related Functions
        public List<DepartmentFunctionDto>? DepartmentFunctions { get; set; } = new List<DepartmentFunctionDto>();
    }

    public class DepartmentFunctionDto
    {
        public int Id { get; set; }
        public Guid DepartmentId { get; set; }
        public int DepartmentDetailsId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string RiskRating { get; set; }
        public string Likehood { get; set; }
        public string Impact { get; set; }
        public string Controls { get; set; }
        public string Responsible { get; set; }
    }

}
