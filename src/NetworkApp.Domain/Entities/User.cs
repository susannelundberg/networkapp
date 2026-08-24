namespace NetworkApp.Domain;

public class User
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public string Title { get; set; }
    public string Employer { get; set; }
    public string Duties { get; set; }
    public string Interests { get; set; }
    public List<Seminar> Participation { get; set; }
    public string PasswordHash { get; set; }
    public Role Role { get; set; } = Role.User;
}
