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

        public override double CalculateBrutto()
        {
            // For climbing, we might have a different calculation, for example, adding a fixed fee for equipment rental
            double equipmentRentalFee = 20.0; // Example fixed fee
            return base.CalculateBrutto() + equipmentRentalFee;
        }
    }
}
