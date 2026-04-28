using System.ComponentModel.DataAnnotations;
using bookingSystemZBC.Domain.Enums;

namespace bookingSystemZBC.Domain.Entities.Activities;

public abstract class Activity
{
    public int Id { get; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public ActivityType Type { get; set; }

    [Range(1, 1000)]
    public int MaxParticipants { get; set; }

    [Range(typeof(double), "0", "999999")]
    public double Price { get; set; }

    public string AdditionalInfoField1 { get; set; } = string.Empty;
    public string Level { get; set; } = string.Empty;


    public ICollection<ActivitySession> Sessions { get; set; } = new List<ActivitySession>();

    protected Activity(string name, ActivityType type, int maxParticipants, double price)
    {
        Name = name;
        Type = type;
        MaxParticipants = maxParticipants;
        Price = price;
    }
    
    public abstract string GetDescription();
    public virtual double CalculateBrutto()
    {
        return Price * MaxParticipants;
    }
}
