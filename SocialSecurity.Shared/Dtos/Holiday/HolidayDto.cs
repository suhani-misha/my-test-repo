using SocialSecurity.Domain.Models.Common;

namespace SocialSecurity.Shared.Dtos.Holiday;
public class HolidayDto
{
    public long Id { get; set; }            // Optional for update
    public DateTime Date { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public string Type { get; set; }           // Enum can be mapped as int or string
}