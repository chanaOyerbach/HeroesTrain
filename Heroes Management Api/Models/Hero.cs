namespace Heroes_Management_Api.Models;

public class Hero
{
    public int HeroId { get; set; }
    public string Name { get; set; }
    public string Ability { get; set; }
    public DateTime StartedTraining { get; set; }
    public string SuitColors { get; set; }
    public decimal StartingPower { get; set; }
    public decimal CurrentPower { get; set; }
    public int TrainingSessionsToday { get; set; }
    public int TrainerId { get; set; }
    public Trainer Trainer { get; set; }
}
