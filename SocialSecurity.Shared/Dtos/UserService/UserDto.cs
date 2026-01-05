namespace SocialSecurity.Shared.Dtos.UserService
{
    public record UserDto(
           Guid Id,
           string Email,
           string Phone,
           string UserName,
           string NormalizedUserName,
           string FirstName,
           string LastName,
           string Gender,
           string EmpId,
           DateTime? DateOfBirth,
           string Department,
           string DepartmentDisplay,
           bool EmailConfirmed,
           bool PhoneConfirmed,
           string? ProfileImage,
           List<string> Roles,
           bool Status,
           DateTime? LastLogin
       );

    public record DepartmentResponseData(
        Guid Id,
        string Name,
        string Description,
        bool IsEnabled,
        string? DeletedBy,
        DateTime? DeletedOn,
        DateTime CreatedOn,
        string CreatedBy,
        DateTime? ModifiedOn,
        string? ModifiedBy
    );
}
