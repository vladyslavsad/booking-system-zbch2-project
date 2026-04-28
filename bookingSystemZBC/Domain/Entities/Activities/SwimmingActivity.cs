using bookingSystemZBC.Domain.Enums;

namespace bookingSystemZBC.Domain.Entities.Activities
{
    public class SwimmingActivity : Activity
    {
        public string AdditionalInfoField1 { get; private set; } = string.Empty;
        public string Level { get; private set; } = string.Empty;

        public SwimmingActivity(string name, ActivityType type, int maxParticipants, double price, string additionalInfoField1, string level)
            : base(name, type, maxParticipants, price)
        {
            AdditionalInfoField1 = additionalInfoField1;
            Level = level;
        }

        public override string GetDescription()
        {
            return $"Swimming Activity: {Name}, Price: {Price}, Max Participants: {MaxParticipants}, Style: {AdditionalInfoField1}, Level: {Level}";
        }
    }
}
