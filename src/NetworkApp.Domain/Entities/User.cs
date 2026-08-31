using Microsoft.AspNetCore.Identity;

namespace NetworkApp.Domain.Entities;

public class User: IdentityUser
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? Title { get; set; }
    public string ?Employer { get; set; }
    public string? Duties { get; set; }
    public string? Interests { get; set; }
    public List<Seminar>? Participation { get; set; }
}
