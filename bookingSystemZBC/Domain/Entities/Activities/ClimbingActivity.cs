using bookingSystemZBC.Domain.Enums;

namespace bookingSystemZBC.Domain.Entities.Activities
{
    public class ClimbingActivity : Activity
    {

        public ClimbingActivity(string name, ActivityType type, int maxParticipants, double price, string additionalInfoField1, string level)
            : base(name, type, maxParticipants, price)
        {
            AdditionalInfoField1 = additionalInfoField1;
            Level = level;
        }
        public override string GetDescription()
        {
            return $"Climbing Activity: {Name}, Price: {Price}, Max Participants: {MaxParticipants}, Does it have instructor: {AdditionalInfoField1}, Level: {Level}";
        }
    }
}
