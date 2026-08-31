using NetworkApp.Domain.Entities;

namespace NetworkApp.Domain;

public class Seminar
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string About { get; set; }
    public string Place { get; set; }
    public DateTime Time { get; set; }
    public List<User> Participants { get; set; }
    public List<string> Lecturer { get; set; }
}
