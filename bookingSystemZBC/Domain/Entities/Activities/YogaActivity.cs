using bookingSystemZBC.Domain.Enums;

namespace bookingSystemZBC.Domain.Entities.Activities
{
    public class YogaActivity : Activity
    {
        public string AddtionalInfoField1 { get; private set; } = string.Empty;
        public string Level { get; private set; } = string.Empty;

        public YogaActivity(string name, ActivityType type, int maxParticipants, double price, string addtionalInfoField1, string level)
            : base(name, type, maxParticipants, price)
        {
            AddtionalInfoField1 = addtionalInfoField1;
            Level = level;
        }

        public override string GetDescription()
        {
            return $"Yoga Activity: {Name}, Price: {Price}, Max Participants: {MaxParticipants}, Style: {AddtionalInfoField1}, Level: {Level}";
        }
    }
}
