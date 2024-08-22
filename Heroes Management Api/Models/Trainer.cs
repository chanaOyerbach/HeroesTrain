namespace Heroes_Management_Api.Models;

public class Trainer
{
    public int TrainerId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; } 
    public ICollection<Hero> Heroes { get; set; }
 
}
